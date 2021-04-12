using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.ChargeStations.Commands.AddChargeStation;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.ChargeStations
{
    using static Testing;

    public class AddChargeStationTests : TestBase
    {
        [Test]
        public void ShouldValidateConnectorMaxCurrent()
        {
            var command = new AddChargeStationCommand
            {
                Name = "Station1",
                ConnectorMaxCurrent = 0
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }
        
        [Test]
        public async Task ShouldCreateChargeStation()
        {
            var group = await AddGroupAsync();

            var command = new AddChargeStationCommand
            {
                GroupId = group.Id,
                ConnectorMaxCurrent = 10,
                Name = "A1"
            };

            var chargeStationId = await SendAsync(command);

            var chargeStation = await FindAsync<ChargeStation>(chargeStationId);
            var connector = await FindAsync<Connector>(1, chargeStationId);

            chargeStation.Should().NotBeNull();
            chargeStation.Name.Should().Be(command.Name);
            connector.MaxCurrent.Should().Be(command.ConnectorMaxCurrent);
        }
    }
}