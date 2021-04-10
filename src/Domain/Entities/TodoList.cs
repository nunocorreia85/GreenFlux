using System.Collections.Generic;
using GreenFlux.Domain.Common;
using GreenFlux.Domain.ValueObjects;

namespace GreenFlux.Domain.Entities
{
    public class TodoList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Colour Colour { get; set; } = Colour.White;

        public IList<TodoItem> Items { get; } = new List<TodoItem>();
    }
}