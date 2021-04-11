using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Application.Dto;
using GreenFlux.Application.Utils;
using GreenFlux.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Connectors.Commands.AddConnector
{
    public class AddConnectorCommandHandler : IRequestHandler<AddConnectorCommand, AddConnectorResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public AddConnectorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddConnectorResponseDto> Handle(AddConnectorCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
            
            if(group == null) throw new NotFoundException(nameof(Group), request.GroupId);

            var connectors = await _context.ChargeStations
                .Where(station => station.GroupId == request.GroupId)
                .SelectMany(station => station.Connectors)
                .ToListAsync(cancellationToken);
            
            var capacityUsed = connectors.Sum(arg => arg.MaxCurrent);

            //if exceeds capacity
            if ((capacityUsed + request.MaxCurrent) > group.Capacity)
            {
                var combinations = CombinationsCalculator.GetCombinations(connectors, request.MaxCurrent);
                return new AddConnectorResponseDto()
                {
                    Suggestions = GetConnectorsToBeRemove(combinations)
                };
            }

            //if can add new connector
            var connector = new Connector()
            {
                ChargeStationId = request.ChargeStationId,
                MaxCurrent = request.MaxCurrent,
                Id = GetConnectorAvailableId(request, connectors)
            };
            
            _context.Connectors.Add(connector);
            await _context.SaveChangesAsync(cancellationToken);
            
            return new AddConnectorResponseDto()
            {
                AddedConnectorId = connector.Id
            };
        }

        private static List<SuggestionDto> GetConnectorsToBeRemove(IEnumerable<Connector[]> combinations)
        {
            return combinations.Select(lst => new SuggestionDto()
            {
                ConnectorsToRemove = lst.Select(c => new ConnectorDto()
                {
                    ConnectorId = c.Id,
                    MaxCurrent = c.MaxCurrent,
                    ChargeStationId = c.ChargeStationId
                })
            }).ToList();
        }

        private static int GetConnectorAvailableId(AddConnectorCommand request, List<Connector> connectors)
        {
            //find a id from 1 till 5
            for (var i = 1; i <= 5; i++)
            {
                if (connectors.Any(connectorDto =>
                    connectorDto.ChargeStationId == request.ChargeStationId && connectorDto.Id == i))
                {
                    continue;
                }
                return i;
            }
            throw new AddConnectorException("Failed to get a new connector id.");
        }

    }
}