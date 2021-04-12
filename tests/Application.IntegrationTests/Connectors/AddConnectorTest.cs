using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Connectors.Commands.AddConnector;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.Connectors
{
    using static Testing;

    public class AddConnectorTests : TestBase
    {
        [Test]
        public void ShouldValidateConnectorMaxCurrent()
        {
            var command = new AddConnectorCommand
            {
                GroupId = 1,
                MaxCurrent = 0,
                ChargeStationId = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateConnector()
        {
            var group = new Group
            {
                Capacity = 100,
                Name = "G1"
            };

            await AddAsync(group);

            var chargeStation = new ChargeStation
            {
                GroupId = group.Id,
                Name = "S1"
            };

            await AddAsync(chargeStation);

            var command = new AddConnectorCommand
            {
                GroupId = group.Id,
                ChargeStationId = chargeStation.Id,
                MaxCurrent = 10
            };

            var response = await SendAsync(command);
            response.AddedConnectorId.Should().NotBeNull();

            var connector = await FindAsync<Connector>(response.AddedConnectorId, chargeStation.Id);

            connector.Should().NotBeNull();
            connector.MaxCurrent.Should().Be(command.MaxCurrent);
        }
    }
}