using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Domain.Entities;
using GreenFlux.Domain.Exceptions;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommand : IRequest
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public float Capacity { get; set; }
    }

    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateGroupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FindAsync(new object[] {request.GroupId}, cancellationToken);

            if (group == null) throw new NotFoundException(nameof(Group), request.GroupId);

            if (request.Capacity < group.Capacity)
            {
                var groupTotalUsedCurrent = _context.Connectors
                    .Where(c => c.ChargeStation.GroupId == request.GroupId)
                    .Sum(c => c.MaxCurrent);
                if (groupTotalUsedCurrent > request.Capacity)
                    throw new EntityUpdateException("Cannot update capacity to less then the total used current.");
            }

            group.Name = request.Name;
            group.Capacity = request.Capacity;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}