using mcr.Data.Helpers;
using mcr.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace mcr.Data;

public class DataContext:DbContext
{
    public DataContext()
    {

    }
    public DataContext(DbContextOptions options) : base(options){

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
            "Server=localhost,1433; Database=mcrdb;Integrated Security=False;MultipleActiveResultSets=true; User Id=sa; Password=Valledupar123; TrustServerCertificate=True;");
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
        builder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter>();
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Encoder> Encoders { get; set; }
    public DbSet<Event> Events{ get; set; }
    public DbSet<Fee> Fees { get; set; }
    public DbSet<Signal> Signals { get; set; }
    public DbSet<Source> Sources { get; set; }
}
