using System.Collections.Generic;
using GreenFlux.Application.Models;
using MediatR;

namespace GreenFlux.Application.ChargeStations.Commands.CreateChargeStation
{
    public class CreateChargeStationCommand : IRequest<long>
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public List<CreateConnectorDto> Connectors { get; set; } = new();
    }
}