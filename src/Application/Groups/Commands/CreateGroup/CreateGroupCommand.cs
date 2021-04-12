using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Domain.Entities;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<long>
    {
        public string Name { get; set; }
        public float Capacity { get; set; }
    }
    
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateGroupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = new Group
            {
                Name = request.Name,
                Capacity = request.Capacity
            };

            _context.Groups.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}