using GreenFlux.Application.Common.Mappings;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Application.Models
{
    public class ConnectorDto : IMapFrom<Connector>
    {
        public int Id { get; set; }
        public float MaxCurrent { get; set; }
    }
}