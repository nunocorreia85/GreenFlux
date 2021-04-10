using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
