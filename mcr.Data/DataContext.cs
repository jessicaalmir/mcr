using mcr.Data.Helpers;
using mcr.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace mcr.Data;

public class DataContext:IdentityDbContext<AppUser, AppRole, int, 
IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataContext()
    {
    }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Encoder> Encoders { get; set; }
    public DbSet<Event> Events{ get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<Signal> Signals { get; set; }
    public DbSet<Source> Sources { get; set; }

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u=>u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Entity<AppRole>()
        .HasMany(ur =>ur.UserRoles)
        .WithOne(u=>u.Role)
        .HasForeignKey(ur=>ur.RoleId)
        .IsRequired();

        
    }
}
