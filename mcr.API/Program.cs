using mcr.Business.IServices;
using mcr.Business.Services;
using mcr.Data;
using mcr.Data.Migrations;
using mcr.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITokenService, TokenService>();
//builder.Services.AddIdentity<AppUser,IdentityRole>(options => options.User.AllowedUserNameCharacters+=" ")
//.AddEntityFrameworkStores<DataContext>()
//.AddDefaultTokenProviders();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
#region Migration
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try{
    var contex = services.GetRequiredService<DataContext>();
    await contex.Database.MigrateAsync();
    await Seed.SeedClients(contex);
    await Seed.SeedSignals(contex);
    await Seed.SeedSources(contex);
    await Seed.SeedEncoders(contex);
}catch(Exception ex){
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error ocurring during migrating data");
}

#endregion

app.Run();
