using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}