using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Domain.Events
{
    public class GroupDeletedEvent : DomainEvent
    {
        public GroupDeletedEvent(Group item)
        {
            Group = item;
        }

        public Group Group { get; }
    }
}