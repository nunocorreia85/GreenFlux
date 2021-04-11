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
                    Id = 1,
                    Capacity = 100,
                    Name = "Nieuw-Vennep"
                });
                context.ChargeStations.AddRange(new ChargeStation
                {
                    Id = 1,
                    Name = "A1",
                    GroupId = 1
                }, new ChargeStation
                {
                    Id = 2,
                    Name = "A2",
                    GroupId = 1
                });
                context.Connectors.AddRange(new Connector
                    {
                        Id = 1,
                        ChargeStationId = 1,
                        MaxCurrent = 10
                    },
                    new Connector
                    {
                        Id = 2,
                        ChargeStationId = 1,
                        MaxCurrent = 14
                    },
                    new Connector
                    {
                        Id = 3,
                        ChargeStationId = 2,
                        MaxCurrent = 32
                    });

                await context.SaveChangesAsync();
            }
        }
    }
}