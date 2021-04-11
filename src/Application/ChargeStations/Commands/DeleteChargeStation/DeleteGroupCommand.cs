using MediatR;

namespace GreenFlux.Application.ChargeStations.Commands.DeleteChargeStation
{
    public class DeleteChargeStationCommand : IRequest
    {
        public long Id { get; set; }
    }
}