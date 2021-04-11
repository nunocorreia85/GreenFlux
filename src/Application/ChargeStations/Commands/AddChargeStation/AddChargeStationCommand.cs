using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.ChargeStations.Models;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Domain.Entities;
using MediatR;

namespace GreenFlux.Application.ChargeStations.Commands.AddChargeStation
{
    public class AddChargeStationCommand : IRequest<long>
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public List<AddChargeStationConnector> Connectors { get; set; } = new();
        
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
}