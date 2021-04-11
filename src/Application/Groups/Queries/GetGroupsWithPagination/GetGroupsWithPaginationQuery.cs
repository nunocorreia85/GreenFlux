using GreenFlux.Application.Common.Models;
using GreenFlux.Application.Models;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetGroupsWithPagination
{
    public class GetGroupsWithPaginationQuery : IRequest<PaginatedList<GroupDto>>
    {
        public long? Id { get; set; }

        public long? ChargeStationId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}