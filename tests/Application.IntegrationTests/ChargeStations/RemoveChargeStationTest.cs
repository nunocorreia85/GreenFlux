using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.ChargeStations.Commands.RemoveChargeStation;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.ChargeStations
{
    using static Testing;
    
    public class RemoveChargeStationTests : TestBase
    {
        [Test]
        public void ShouldRequireValidChargeStationId()
        {
            var command = new RemoveChargeStationCommand { Id = 99 };

            FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRemoveChargeStation()
        {
            var group = new Group()
            {
                Capacity = 100,
                Name = "G1"
            };
            
            await AddAsync(group);

            var chargeStation = new ChargeStation()
            {
                GroupId = group.Id,
                Name = "S1",
                Connectors = new List<Connector>()
                {
                    new Connector()
                    {
                        Id = 1,
                        MaxCurrent = 10
                    }
                }
            };

            await AddAsync(chargeStation);
            var id = chargeStation.Id;
            
            await SendAsync(new RemoveChargeStationCommand 
            { 
                Id =  id
            });

            chargeStation = await FindAsync<ChargeStation>(id);
            chargeStation.Should().BeNull();
        }
    }
}