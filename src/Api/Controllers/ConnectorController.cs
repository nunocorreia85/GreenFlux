using System.Threading.Tasks;
using GreenFlux.Application.Connectors.Commands.AddConnector;
using GreenFlux.Application.Connectors.Commands.RemoveConnector;
using GreenFlux.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux.Api.Controllers
{
    public class ConnectorController : ApiControllerBase
    {
        [HttpPost]
        public async Task<AddConnectorResponseDto> Add(AddConnectorCommand command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpDelete("{chargeStationId}/{connectorId}")]
        public async Task<ActionResult> Remove(long chargeStationId, int connectorId)
        {
            await Mediator.Send(new RemoveConnectorCommand() {ChargeStationId = chargeStationId, ConnectorId = connectorId});

            return NoContent();
        }
    }
}