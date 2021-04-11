using System.Collections.Generic;
using GreenFlux.Application.Common.Mappings;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Application.Dto
{
    public class GroupDto : IMapFrom<Group>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Capacity { get; set; }
        public List<ChargeStationDto> ChargeStations { get; set; }
    }
}