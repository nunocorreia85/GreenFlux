using System.Threading.Tasks;
using GreenFlux.Application.ChargeStations.Commands.AddChargeStation;
using GreenFlux.Application.ChargeStations.Commands.RemoveChargeStation;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux.Api.Controllers
{
    public class ChargeStationController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<long>> Create(AddChargeStationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new RemoveChargeStationCommand {ChargeStationId = id});

            return NoContent();
        }
    }
}