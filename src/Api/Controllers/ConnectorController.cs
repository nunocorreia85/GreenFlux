using System.Threading.Tasks;
using GreenFlux.Application.Connectors.Commands.AddConnector;
using GreenFlux.Application.Connectors.Commands.RemoveConnector;
using GreenFlux.Application.Connectors.Commands.UpdateConnector;
using GreenFlux.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux.Api.Controllers
{
    public class ConnectorController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Add(AddConnectorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{chargeStationId}/{connectorId}")]
        public async Task<ActionResult> Remove(long chargeStationId, int connectorId)
        {
            await Mediator.Send(new RemoveConnectorCommand
                {ChargeStationId = chargeStationId, ConnectorId = connectorId});

            return NoContent();
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(UpdateConnectorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}