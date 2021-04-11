using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ChargeStation> ChargeStations { get; set; }

        DbSet<Group> Groups { get; set; }

        DbSet<Connector> Connectors { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}