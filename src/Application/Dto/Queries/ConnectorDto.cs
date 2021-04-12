using AutoMapper;
using GreenFlux.Application.Common.Mappings;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Application.Dto.Queries
{
    public class ConnectorDto : DtoBase, IMapFrom<Connector>
    {
        public int ConnectorId { get; set; }
        public long ChargeStationId { get; set; }
        public float MaxCurrent { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Connector, ConnectorDto>()
                .ForMember(d => d.ConnectorId,
                    opt => opt.MapFrom(s => s.Id));
        }
    }
}