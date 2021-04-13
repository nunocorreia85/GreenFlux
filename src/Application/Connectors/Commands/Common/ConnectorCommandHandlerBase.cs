using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Application.Dto.Commands;
using GreenFlux.Application.Dto.Queries;
using GreenFlux.Domain.Entities;
using GreenFlux.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Connectors.Commands.Common
{
    public class ConnectorCommandHandlerBase
    {
        protected IApplicationDbContext _context;

        protected ConnectorCommandHandlerBase(IApplicationDbContext context)
        {
            _context = context;
        }

        protected static List<SuggestionDto> GetConnectorsToBeRemove(IEnumerable<Connector[]> combinations)
        {
            return combinations
                .OrderBy(c => c.Length)
                .Select(lst => new SuggestionDto
                {
                    ConnectorsToRemove = lst.Select(c => new ConnectorDto
                    {
                        ConnectorId = c.Id,
                        MaxCurrent = c.MaxCurrent,
                        ChargeStationId = c.ChargeStationId
                    }).ToList()
                }).ToList();
        }

        protected static int GetConnectorAvailableId(List<Connector> connectors, long requestChargeStationId)
        {
            //find a id from 1 till 5
            for (var i = 1; i <= 5; i++)
            {
                if (connectors.Any(connectorDto =>
                    connectorDto.ChargeStationId == requestChargeStationId && connectorDto.Id == i))
                    continue;
                return i;
            }

            throw new EntityKeyGeneratorException("Failed to generate a new connector id.");
        }

        protected async Task<List<Connector>> GetGroupConnectors(long requestGroupId,
            CancellationToken cancellationToken)
        {
            var connectors = await _context.ChargeStations
                .Where(station => station.GroupId == requestGroupId)
                .SelectMany(station => station.Connectors)
                .ToListAsync(cancellationToken);
            return connectors;
        }

        protected async Task<Group> GetGroup(long requestGroupId, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FindAsync(new object[] {requestGroupId},
                cancellationToken);

            if (group == null) throw new NotFoundException(nameof(Group), requestGroupId);
            return group;
        }

        protected static bool ExceedsGroupCapacity(List<Connector> connectors, float connectorMaxCurrent,
            float groupMaxCapacity)
        {
            return connectors.Sum(arg => arg.MaxCurrent) + connectorMaxCurrent > groupMaxCapacity;
        }
    }
}