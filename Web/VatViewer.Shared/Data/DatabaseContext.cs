using Microsoft.EntityFrameworkCore;
using VatViewer.Shared.Models;

namespace VatViewer.Shared.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Atis> Atis { get; set; }
    public DbSet<Controller> Controllers { get; set; }
    public DbSet<FlightPlan> FlightPlans { get; set; }
    public DbSet<Pilot> Pilots { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Prefile> Prefliles { get; set; }
}
