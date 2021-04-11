using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Domain.Events
{
    public class ChargeStationRemovedEvent : DomainEvent
    {
        public ChargeStationRemovedEvent(ChargeStation chargeStation)
        {
            ChargeStation = chargeStation;
        }

        public ChargeStation ChargeStation { get; }
    }
}