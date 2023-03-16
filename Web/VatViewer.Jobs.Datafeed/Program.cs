using dotenv.net;
using Newtonsoft.Json;
using StackExchange.Redis;
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

            var client = new HttpClient();
            Console.WriteLine("Getting v3 url from status");
            var statusResponse = await client.GetStringAsync(Environment.GetEnvironmentVariable("STATUS_URL") ??
                throw new ArgumentNullException("STATUS_URL env variable not found"));
            var status = JsonConvert.DeserializeObject<Status>(statusResponse);

            Console.WriteLine("Getting datafeed");
            var datafeedResponse = await client.GetStringAsync(status?.Data?.V3?.First());
            var datafeed = JsonConvert.DeserializeObject<Feed>(datafeedResponse);

            Console.WriteLine("Adding general data to redis");
            await redis.StringSetAsync("general", JsonConvert.SerializeObject(datafeed?.General), TimeSpan.FromMinutes(2));

            Console.WriteLine($"Adding {datafeed?.Pilots?.Count} pilots to redis");
            await redis.StringSetAsync("pilots", JsonConvert.SerializeObject(datafeed?.Pilots), TimeSpan.FromMinutes(2));

            Console.WriteLine($"Adding {datafeed?.Controllers?.Count} controllers to redis");
            await redis.StringSetAsync("controllers", JsonConvert.SerializeObject(datafeed?.Controllers), TimeSpan.FromMinutes(2));

            Console.WriteLine($"Adding {datafeed?.Atis?.Count} atis's to redis");
            await redis.StringSetAsync("atis", JsonConvert.SerializeObject(datafeed?.Atis), TimeSpan.FromMinutes(2));

            Console.WriteLine($"Adding {datafeed?.Prefiles?.Count} prefiles to redis");
            await redis.StringSetAsync("prefiles", JsonConvert.SerializeObject(datafeed?.Prefiles), TimeSpan.FromMinutes(2));

            Console.WriteLine("Done");

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