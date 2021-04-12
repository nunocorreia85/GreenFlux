﻿using System.Collections.Generic;
using AutoMapper;
using GreenFlux.Application.Common.Mappings;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Application.Dto
{
    public class ChargeStationDto : IMapFrom<ChargeStation>
    {
        public long GroupId { get; set; }
        public long ChargeStationId { get; set; }
        public string Name { get; set; }
        public List<ConnectorDto> Connectors { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChargeStation, ChargeStationDto>()
                .ForMember(d => d.ChargeStationId,
                    opt => opt.MapFrom(s => s.Id));
        }
    }
}