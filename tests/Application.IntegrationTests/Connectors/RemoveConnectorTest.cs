using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Connectors.Commands.RemoveConnector;
using GreenFlux.Domain.Entities;
using GreenFlux.Domain.Exceptions;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.Connectors
{
    using static Testing;

    public class RemoveConnectorTests : TestBase
    {
        [Test]
        public void ShouldRequireValidConnectorId()
        {
            var command = new RemoveConnectorCommand
            {
                ConnectorId = 1, 
                ChargeStationId = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldHaveAtLeastOneConnector()
        {
            var group = await AddGroupAsync();

            var chargeStation = await AddChargeStationAsync(group.Id);
            
            var connector = await AddConnectorAsync(chargeStation.Id, 1, 10);

            var removeConnectorCommand = new RemoveConnectorCommand
            {
                ConnectorId = connector.Id, ChargeStationId = chargeStation.Id
            };
            
            FluentActions.Invoking(() =>
                SendAsync(removeConnectorCommand)).Should().Throw<EntityRemoveException>();
        }
        
        [Test]
        public async Task ShouldRemoveConnector()
        {
            var group = await AddGroupAsync();

            var chargeStation = await AddChargeStationAsync(group.Id);
            
            var connector = await AddConnectorAsync(chargeStation.Id, 1, 10);

            await AddConnectorAsync(chargeStation.Id, 2, 10);
            
            await SendAsync(new RemoveConnectorCommand
            {
                ConnectorId = connector.Id, ChargeStationId = chargeStation.Id
            });

            connector = await FindAsync<Connector>(new object[]{connector.Id, chargeStation.Id});
            connector.Should().BeNull();
        }
    }
}