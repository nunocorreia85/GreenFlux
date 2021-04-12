using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Domain.Entities;
using GreenFlux.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Groups.Commands.DeleteGroup
{
    public class DeleteGroupCommand : IRequest
    {
        public long Id { get; set; }
        
        public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteGroupCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

                if (group == null) throw new NotFoundException(nameof(Group), request.Id);

                var chargeStationIds = await _context.ChargeStations
                    .Where(c => c.Id == request.Id)
                    .Select(c => c.Id)
                    .ToListAsync(cancellationToken);

                var connectorIds = await _context.Connectors
                    .Where(c => chargeStationIds.Contains(c.Id))
                    .Select(c => c.Id)
                    .ToListAsync(cancellationToken);

                _context.Connectors.RemoveRange(connectorIds.Select(i => new Connector
                {
                    Id = i
                }));

                _context.ChargeStations.RemoveRange(chargeStationIds.Select(i => new ChargeStation
                {
                    Id = i
                }));

                _context.Groups.Remove(group);

                group.DomainEvents.Add(new GroupDeletedEvent(group));

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}