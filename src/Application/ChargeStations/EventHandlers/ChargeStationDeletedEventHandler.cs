using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Models;
using GreenFlux.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GreenFlux.Application.ChargeStations.EventHandlers
{
    public class
        ChargeStationDeletedEventHandler : INotificationHandler<DomainEventNotification<ChargeStationRemovedEvent>>
    {
        private readonly ILogger<ChargeStationDeletedEventHandler> _logger;

        public ChargeStationDeletedEventHandler(ILogger<ChargeStationDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ChargeStationRemovedEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}