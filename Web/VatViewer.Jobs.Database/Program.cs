using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using VatViewer.Shared.Data;
using VatViewer.Shared.Datafeed;

DotEnv.Load();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(async services =>
    {
        try
        {
            var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ??
                throw new ArgumentNullException("REDIS_HOST env variable not found");
            var redis = ConnectionMultiplexer.Connect(redisHost).GetDatabase();

            var options = new DbContextOptionsBuilder<DatabaseContext>();
            options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                throw new ArgumentException("CONNECTION_STRING env variable not found"));
            var context = new DatabaseContext(options.Options);

            // Process pilots
            Console.WriteLine("Processing pilots");
            var updated = 0;
            var added = 0;
            var removed = 0;
            var redisPilotsRaw = await redis.StringGetAsync("pilots");
            if (redisPilotsRaw.HasValue)
            {
                var redisPilots = JsonConvert.DeserializeObject<IList<Pilot>>(redisPilotsRaw!);
                if (redisPilots != null)
                {
                    foreach (var entry in redisPilots)
                    {
                        var databasePilot = await context.Pilots.FirstOrDefaultAsync(
                                x => x.Callsign == entry.Callsign &&
                                x.Name == entry.Name &&
                                x.Cid == entry.Cid &&
                                x.LogonTime == entry.LogonTime
                            );
                        if (databasePilot != null)
                        {
                            // Pilot found in db, so update data
                            if (databasePilot.FlightPlan != null)
                            {
                                databasePilot.FlightPlan.FlightRules = entry.FlightPlan?.FlightRules ?? string.Empty;
                                databasePilot.FlightPlan.Aircraft = entry.FlightPlan?.Aircraft ?? string.Empty;
                                databasePilot.FlightPlan.Departure = entry.FlightPlan?.Departure ?? string.Empty;
                                databasePilot.FlightPlan.Arrival = entry.FlightPlan?.Arrival ?? string.Empty;
                                databasePilot.FlightPlan.Route = entry.FlightPlan?.Route ?? string.Empty;
                            }
                            databasePilot.LogonTime = DateTimeOffset.UtcNow;
                            await context.Positions.AddAsync(new VatViewer.Shared.Models.Position
                            {
                                Pilot = databasePilot,
                                Latitude = entry.Latitude,
                                Longitude = entry.Longitude,
                                Altitude = entry.Altitude,
                                GroundSpeed = entry.GroundSpeed,
                                Heading = entry.Heading
                            });
                            await context.SaveChangesAsync();
                            updated++;
                        }
                        else
                        {
                            // Add new pilot because it wasn't found in the db
                            var plan = await context.FlightPlans.AddAsync(new VatViewer.Shared.Models.FlightPlan
                            {
                                Cid = entry.Cid,
                                FlightRules = entry.FlightPlan?.FlightRules ?? string.Empty,
                                Aircraft = entry.FlightPlan?.Aircraft ?? string.Empty,
                                Departure = entry.FlightPlan?.Departure ?? string.Empty,
                                Arrival = entry.FlightPlan?.Arrival ?? string.Empty,
                                Route = entry.FlightPlan?.Route ?? string.Empty
                            });
                            await context.SaveChangesAsync();
                            await context.Pilots.AddAsync(new VatViewer.Shared.Models.Pilot
                            {
                                Cid = entry.Cid,
                                Callsign = entry.Callsign,
                                Name = entry.Name,
                                FlightPlan = plan.Entity,
                                LogonTime = entry.LogonTime,
                                LogoffTime = DateTimeOffset.UtcNow,
                            });
                            await context.SaveChangesAsync();
                            added++;
                        }
                    }

                    var activePilots = await context.Pilots.Where(x => x.Length == null).ToListAsync();
                    foreach (var entry in activePilots)
                    {
                        var redisPilotExists = redisPilots.Any(
                                x => x.Callsign == entry.Callsign &&
                                x.Name == entry.Name &&
                                x.Cid == entry.Cid &&
                                x.LogonTime == entry.LogonTime
                            );
                        if (!redisPilotExists)
                        {
                            entry.LogoffTime = DateTimeOffset.UtcNow;
                            entry.Length = entry.LogoffTime - entry.LogonTime;
                            await context.SaveChangesAsync();
                            removed++;
                        }
                    }
                }
            }
            Console.WriteLine($"Added: {added}");
            Console.WriteLine($"Updated: {updated}");
            Console.WriteLine($"Removed: {removed}");
            Environment.Exit(0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred getting the datafeed: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Environment.Exit(1);
        }
    })
    .Build();

host.Run();
