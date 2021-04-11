using System.Collections.Generic;
using GreenFlux.Application.Common.Mappings;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Application.Models
{
    public class ChargeStationDto : IMapFrom<ChargeStation>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long GroupId { get; set; }
        public List<ConnectorDto> Connectors { get; set; }
    }
}