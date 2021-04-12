using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Application.Connectors.Commands.Common;
using GreenFlux.Application.Dto;
using GreenFlux.Application.Dto.Commands;
using GreenFlux.Application.Utils;
using GreenFlux.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Connectors.Commands.AddConnector
{
    public class AddConnectorCommand : IRequest<AddConnectorResponseDto>
    {
        public long GroupId { get; set; }
        public long ChargeStationId { get; set; }
        public float MaxCurrent { get; set; }
    }

    public class AddConnectorCommandHandler : ConnectorCommandHandlerBase, 
        IRequestHandler<AddConnectorCommand, AddConnectorResponseDto>
    {
        public AddConnectorCommandHandler(IApplicationDbContext context) : base(context)
        { }

        public async Task<AddConnectorResponseDto> Handle(AddConnectorCommand request,
            CancellationToken cancellationToken)
        {
            var group = await GetGroup(request.GroupId, cancellationToken);
            var connectors = await GetGroupConnectors(request.GroupId, cancellationToken);

            //if exceeds capacity
            if (ExceedsGroupCapacity(connectors, request.MaxCurrent, group.Capacity))
            {
                var combinations = CombinationsCalculator.GetCombinations(connectors, request.MaxCurrent);
                return new AddConnectorResponseDto
                {
                    Suggestions = GetConnectorsToBeRemove(combinations)
                };
            }
            
            //TODO:if exceeds the max number of connectors per station
            
            //if can add new connector
            var connector = new Connector
            {
                ChargeStationId = request.ChargeStationId,
                MaxCurrent = request.MaxCurrent,
                Id = GetConnectorAvailableId(connectors, request.ChargeStationId)
            };

            _context.Connectors.Add(connector);
            await _context.SaveChangesAsync(cancellationToken);

            return new AddConnectorResponseDto
            {
                NewConnectorId = connector.Id
            };
        }
    }
}