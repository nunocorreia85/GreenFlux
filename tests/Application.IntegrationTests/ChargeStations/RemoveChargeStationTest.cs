using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.ChargeStations.Commands.RemoveChargeStation;
using GreenFlux.Application.Common.Exceptions;
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
            var command = new RemoveChargeStationCommand {ChargeStationId = 99};

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRemoveChargeStation()
        {
            var group = await AddGroupAsync();

            var chargeStation = new ChargeStation
            {
                GroupId = group.Id,
                Name = "S1",
                Connectors = new List<Connector>
                {
                    new() {Id = 1, MaxCurrent = 10}
                }
            };

            await AddAsync(chargeStation);
            var id = chargeStation.Id;

            await SendAsync(new RemoveChargeStationCommand
            {
                ChargeStationId = id
            });

            chargeStation = await FindAsync<ChargeStation>(id);
            chargeStation.Should().BeNull();
        }
    }
}