using GreenFlux.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace GreenFlux.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
