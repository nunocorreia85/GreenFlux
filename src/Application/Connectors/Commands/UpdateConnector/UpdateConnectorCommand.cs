using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Application.Connectors.Commands.Common;
using GreenFlux.Application.Dto.Commands;
using GreenFlux.Application.Utils;
using MediatR;

namespace GreenFlux.Application.Connectors.Commands.UpdateConnector
{
    public class UpdateConnectorCommand : IRequest<UpdateConnectorResponseDto>
    {
        public long GroupId { get; set; }
        public long ChargeStationId { get; set; }
        public int ConnectorId { get; set; }
        public float MaxCurrent { get; set; }
    }

    public class UpdateConnectorCommandHandler : ConnectorCommandHandlerBase,
        IRequestHandler<UpdateConnectorCommand, UpdateConnectorResponseDto>
    {
        public UpdateConnectorCommandHandler(IApplicationDbContext context) : base(context)
        {
        }

        public async Task<UpdateConnectorResponseDto> Handle(UpdateConnectorCommand request,
            CancellationToken cancellationToken)
        {
            var connector = await _context.Connectors.FindAsync(
                new object[] {request.ConnectorId, request.ChargeStationId}, cancellationToken);

            var group = await GetGroup(request.GroupId, cancellationToken);
            var connectors = await GetGroupConnectors(request.GroupId, cancellationToken);

            //if exceeds capacity
            if (ExceedsGroupCapacity(connectors, request.MaxCurrent, group.Capacity))
            {
                var combinations = CombinationsCalculator.GetCombinations(connectors, request.MaxCurrent);
                return new UpdateConnectorResponseDto
                {
                    Suggestions = GetConnectorsToBeRemove(combinations)
                };
            }

            connector.MaxCurrent = request.MaxCurrent;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateConnectorResponseDto();
        }
    }
}