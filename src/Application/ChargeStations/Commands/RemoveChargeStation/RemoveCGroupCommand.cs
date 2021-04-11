using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Domain.Entities;
using GreenFlux.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.ChargeStations.Commands.RemoveChargeStation
{
    public class RemoveChargeStationCommand : IRequest
    {
        public long Id { get; set; }
        
        public class RemoveChargeStationCommandHandler : IRequestHandler<RemoveChargeStationCommand>
        {
            private readonly IApplicationDbContext _context;

            public RemoveChargeStationCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveChargeStationCommand request, CancellationToken cancellationToken)
            {
                var chargeStation = await _context.ChargeStations
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

                if (chargeStation == null) throw new NotFoundException(nameof(chargeStation), request.Id);

                var connectorIds = await _context.Connectors
                    .Where(c => c.ChargeStationId == request.Id)
                    .Select(c => c.Id)
                    .ToListAsync(cancellationToken);

                _context.Connectors.RemoveRange(connectorIds.Select(i => new Connector
                {
                    Id = i
                }));

                _context.ChargeStations.Remove(chargeStation);

                chargeStation.DomainEvents.Add(new ChargeStationRemovedEvent(chargeStation));

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}