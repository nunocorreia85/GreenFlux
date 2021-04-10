using GreenFlux.Domain.Common;
using System.Threading.Tasks;

namespace GreenFlux.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
