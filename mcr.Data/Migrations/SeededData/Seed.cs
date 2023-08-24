using System;
using System.Text.Json;
using mcr.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace mcr.Data.Migrations
{
    public class Seed
    {
        public static async Task SeedClients(DataContext context){
            if(await context.Clients.AnyAsync()) return;
            
            var clientData =  await File.ReadAllTextAsync("../mcr.Data/Migrations/SeededData/ClientsData.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive=true};
            var clients = JsonSerializer.Deserialize<List<Client>>(clientData);
            foreach(var client in clients){
                context.Clients.Add(client);
            }   
            await context.SaveChangesAsync();
        }

        public static async Task SeedEncoders(DataContext context){
            if(await context.Encoders.AnyAsync()) return;
            
            var encoderData =  await File.ReadAllTextAsync("../mcr.Data/Migrations/SeededData/EncodersData.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive=true};
            var encoders = JsonSerializer.Deserialize<List<Encoder>>(encoderData);
            foreach(var encoder in encoders){
                context.Encoders.Add(encoder);
            }   
            await context.SaveChangesAsync();
        }

        public static async Task SeedSignals(DataContext context){
            if(await context.Signals.AnyAsync()) return;
            
            var signalData =  await File.ReadAllTextAsync("../mcr.Data/Migrations/SeededData/SignalsData.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive=true};
            var signals = JsonSerializer.Deserialize<List<Signal>>(signalData);
            foreach(var signal in signals){
                context.Signals.Add(signal);
            }   
            await context.SaveChangesAsync();
        }

        public static async Task SeedSources(DataContext context){
            if(await context.Sources.AnyAsync()) return;
            
            var sourceData =  await File.ReadAllTextAsync("../mcr.Data/Migrations/SeededData/SourcesData.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive=true};
            var sources = JsonSerializer.Deserialize<List<Source>>(sourceData);
            foreach(var source in sources){
                context.Sources.Add(source);
            }   
            await context.SaveChangesAsync();
        }
    }
}