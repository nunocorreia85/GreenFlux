using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Domain.Entities;
using MediatR;

namespace GreenFlux.Application.ChargeStations.Commands.AddChargeStation
{
    public class AddChargeStationCommand : IRequest<long>
    {
        public long GroupId { get; set; }
        public string Name { get; set; }

        public float ConnectorMaxCurrent { get; set; }
    }
    
    public class AddChargeStationCommandHandler : IRequestHandler<AddChargeStationCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public AddChargeStationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(AddChargeStationCommand request, CancellationToken cancellationToken)
        {
            var chargeStation = new ChargeStation
            {
                Name = request.Name,
                GroupId = request.GroupId,
                Connectors = new List<Connector>
                {
                    new()
                    {
                        Id = 1,
                        MaxCurrent = request.ConnectorMaxCurrent
                    }
                }
            };

            _context.ChargeStations.Add(chargeStation);

            await _context.SaveChangesAsync(cancellationToken);

            return chargeStation.Id;
        }
    }
}