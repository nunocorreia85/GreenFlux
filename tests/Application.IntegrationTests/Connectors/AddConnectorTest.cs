using System.Linq;
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
            var group = await AddGroupAsync();

            var chargeStation = await AddChargeStationAsync(group.Id);

            var command = new AddConnectorCommand
            {
                GroupId = group.Id,
                ChargeStationId = chargeStation.Id,
                MaxCurrent = 10
            };

            var response = await SendAsync(command);
            response.NewConnectorId.Should().NotBeNull();

            var connector = await FindAsync<Connector>(response.NewConnectorId, chargeStation.Id);

            connector.Should().NotBeNull();
            connector.MaxCurrent.Should().Be(command.MaxCurrent);
        }
        
        [Test]
        public async Task ShouldGetSuggestionIfCannotAdd()
        {
            var group = await AddGroupAsync();

            var chargeStation = await AddChargeStationAsync(group.Id);

            await AddConnectorAsync(chargeStation.Id, 1, 10);
            await AddConnectorAsync(chargeStation.Id, 2, 90);
            var command = new AddConnectorCommand
            {
                GroupId = group.Id,
                ChargeStationId = chargeStation.Id,
                MaxCurrent = 10
            };

            var response = await SendAsync(command);
            response.NewConnectorId.Should().BeNull();
            response.Suggestions.Count.Should().Be(1);
            response.Suggestions[0].ConnectorsToRemove.Count.Should().Be(1);
            response.Suggestions[0].ConnectorsToRemove[0].ConnectorId.Should().Be(1);
            var connector = await FindAsync<Connector>(response.NewConnectorId, chargeStation.Id);
            connector.Should().BeNull();
        }
    }
}