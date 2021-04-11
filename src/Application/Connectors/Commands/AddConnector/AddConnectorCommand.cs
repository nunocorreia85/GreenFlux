using GreenFlux.Application.Dto;
using MediatR;

namespace GreenFlux.Application.Connectors.Commands.AddConnector
{
    public class AddConnectorCommand : IRequest<AddConnectorResponseDto>
    {
        public long GroupId { get; set; }
        public long ChargeStationId { get; set; }
        public float MaxCurrent { get; set; }
    }
}