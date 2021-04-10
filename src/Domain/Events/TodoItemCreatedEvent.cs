using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}