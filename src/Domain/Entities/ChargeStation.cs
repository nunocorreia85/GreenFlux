using System.Collections.Generic;
using GreenFlux.Domain.Common;

namespace GreenFlux.Domain.Entities
{
    public class ChargeStation : AuditableEntity, IHasDomainEvent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long GroupId { get; set; }

        public Group Group { get; set; }

        public List<Connector> Connectors { get; set; } = new();
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}