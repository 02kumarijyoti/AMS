using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entintes;
using Microsoft.Extensions.Logging;

namespace API.Data
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Resources.Any())
                {
                    var resourcesData = File.ReadAllText("../API/Data/SeedData/Resource.json");
                    var resources = JsonSerializer.Deserialize<List<Resource>>(resourcesData);
                    foreach (var item in resources)
                    {
                        context.Resources.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
                 if (!context.Projects.Any())
                {
                    var projectsData = File.ReadAllText("../API/Data/SeedData/Project.json");
                    var projects = JsonSerializer.Deserialize<List<Project>>(projectsData);
                    foreach (var item in projects)
                    {
                        context.Projects.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger= loggerFactory.CreateLogger<DataContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}