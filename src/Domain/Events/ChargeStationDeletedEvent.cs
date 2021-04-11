using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Domain.Events
{
    public class ChargeStationDeletedEvent : DomainEvent
    {
        public ChargeStationDeletedEvent(ChargeStation chargeStation)
        {
            ChargeStation = chargeStation;
        }

        public ChargeStation ChargeStation { get; }
    }
}