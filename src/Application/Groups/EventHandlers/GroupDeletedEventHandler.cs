﻿using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Models;
using GreenFlux.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GreenFlux.Application.Groups.EventHandlers
{
    public class GroupDeletedEventHandler : INotificationHandler<DomainEventNotification<GroupDeletedEvent>>
    {
        private readonly ILogger<GroupDeletedEventHandler> _logger;

        public GroupDeletedEventHandler(ILogger<GroupDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<GroupDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}