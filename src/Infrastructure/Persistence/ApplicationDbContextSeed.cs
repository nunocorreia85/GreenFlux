using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Groups.Any())
            {
                context.Groups.Add(new Group
                {
                    Capacity = 100,
                    Name = "Nieuw-Vennep",
                    ChargeStations = new List<ChargeStation>
                    {
                        new()
                        {
                            Name = "A1",
                            Connectors = new List<Connector>
                            {
                                new()
                                {
                                    Id = 1,
                                    MaxCurrent = 10
                                },
                                new()
                                {
                                    Id = 2,
                                    MaxCurrent = 20
                                }
                            }
                        },
                        new()
                        {
                            Name = "A2",
                            Connectors = new List<Connector>
                            {
                                new()
                                {
                                    Id = 1,
                                    MaxCurrent = 30
                                }
                            }
                        }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}