using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Application.Connectors.Commands.Common;
using GreenFlux.Domain.Entities;
using GreenFlux.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Connectors.Commands.RemoveConnector
{
    public class RemoveConnectorCommand : IRequest
    {
        public int ConnectorId { get; set; }

        public long ChargeStationId { get; set; }
    }

    public class RemoveConnectorCommandHandler : ConnectorCommandHandlerBase,
        IRequestHandler<RemoveConnectorCommand>
    {
        public RemoveConnectorCommandHandler(IApplicationDbContext context) : base(context)
        {
        }

        public async Task<Unit> Handle(RemoveConnectorCommand request, CancellationToken cancellationToken)
        {
            var stationConnectorIds = await _context.Connectors
                .Where(c => c.ChargeStationId == request.ChargeStationId)
                .Select(c => c.Id)
                .ToListAsync(cancellationToken);

            if (!stationConnectorIds.Contains(request.ConnectorId))
                throw new NotFoundException(nameof(Connector), request.ConnectorId);

            if (stationConnectorIds.Count() == 1)
                throw new EntityRemoveException("Charge station cannot exist without a connector");

            _context.Connectors.Remove(new Connector
            {
                Id = request.ConnectorId,
                ChargeStationId = request.ChargeStationId
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}