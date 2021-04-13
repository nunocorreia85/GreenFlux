using System.Collections.Generic;
using AutoMapper;
using GreenFlux.Application.Common.Mappings;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Application.Dto.Queries
{
    public class GroupDto : DtoBase, IMapFrom<Group>
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public float Capacity { get; set; }
        public List<ChargeStationDto> ChargeStations { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Group, GroupDto>()
                .ForMember(d => d.GroupId,
                    opt =>
                        opt.MapFrom(s => s.Id));
        }
    }
}