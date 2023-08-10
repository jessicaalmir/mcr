using mcr.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace mcr.Data;

public class DataContext:DbContext
{
    public DataContext(){

    }
    public DataContext(DbContextOptions options) : base(options){

    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Content> Contents {get; set; }
    public DbSet<ContentType> ContentTypes {get; set;}    
    public DbSet<Encoder> Encoders { get; set; }
    public DbSet<Signal> Signals { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<Transmission> Transmissions { get; set; }
    public DbSet<TransmissionSignal> TransmissionSignals { get; set; }
}
