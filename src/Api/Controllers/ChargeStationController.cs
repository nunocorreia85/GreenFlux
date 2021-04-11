using System.Threading.Tasks;
using GreenFlux.Application.ChargeStations.Commands.CreateChargeStation;
using GreenFlux.Application.ChargeStations.Commands.DeleteChargeStation;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux.Api.Controllers
{
    public class ChargeStationController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateChargeStationCommand command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteChargeStationCommand() {Id = id});

            return NoContent();
        }
    }
}