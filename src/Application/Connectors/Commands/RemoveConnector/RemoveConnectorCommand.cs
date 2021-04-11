using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Connectors.Commands.RemoveConnector
{
    public class RemoveConnectorCommand : IRequest
    {
        public int ConnectorId { get; set; }

        public long ChargeStationId { get; set; }

        public class RemoveConnectorCommandHandler : IRequestHandler<RemoveConnectorCommand>
        {
            private readonly IApplicationDbContext _context;

            public RemoveConnectorCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveConnectorCommand request, CancellationToken cancellationToken)
            {
                var connector = await _context.Connectors
                    .FirstOrDefaultAsync(s => s.Id == request.ConnectorId && s.ChargeStationId == request.ChargeStationId,
                        cancellationToken);

                if (connector == null) throw new NotFoundException(nameof(connector), request.ConnectorId);

                _context.Connectors.Remove(connector);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}