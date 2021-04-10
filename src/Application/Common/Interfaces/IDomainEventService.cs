using System.Threading.Tasks;
using GreenFlux.Domain.Common;

namespace GreenFlux.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}