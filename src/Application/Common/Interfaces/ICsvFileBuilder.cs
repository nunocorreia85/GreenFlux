using System.Collections.Generic;
using GreenFlux.Application.TodoLists.Queries.ExportTodos;

namespace GreenFlux.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}