using System.Collections.Generic;
using GreenFlux.Domain.Common;

namespace GreenFlux.Domain.Entities
{
    public class Group : AuditableEntity, IHasDomainEvent
    {
        /// <summary>
        ///     Use long instead of guid to avoid db fragmentation on clustered GUID index
        /// </summary>
        public long Id { get; set; }

        public string Name { get; set; }
        public float Capacity { get; set; }
        public List<ChargeStation> ChargeStations { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}