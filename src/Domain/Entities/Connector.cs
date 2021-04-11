using GreenFlux.Domain.Common;

namespace GreenFlux.Domain.Entities
{
    public class Connector : AuditableEntity
    {
        public int Id { get; set; }
        public float MaxCurrent { get; set; }
        public long ChargeStationId { get; set; }

        public ChargeStation ChargeStation { get; set; }
    }
}