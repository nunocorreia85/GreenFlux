using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Domain.Entities;
using MediatR;

namespace GreenFlux.Application.ChargeStations.Commands.CreateChargeStation
{
    public class CreateChargeStationCommandHandler : IRequestHandler<CreateChargeStationCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateChargeStationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateChargeStationCommand request, CancellationToken cancellationToken)
        {
            var chargeStation = new ChargeStation
            {
                Name = request.Name,
                GroupId = request.GroupId
            };

            for (var i = 0; i < request.Connectors.Count; i++)
            {
                chargeStation.Connectors.Add(new Connector
                {
                    Id = i + 1,
                    MaxCurrent = request.Connectors[i].MaxCurrent
                });
            }

            _context.ChargeStations.Add(chargeStation);
            
            await _context.SaveChangesAsync(cancellationToken);

            return chargeStation.Id;
        }
    }
}