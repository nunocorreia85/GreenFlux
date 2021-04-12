using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Application.Common.Mappings;
using GreenFlux.Application.Common.Models;
using GreenFlux.Application.Dto;
using GreenFlux.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Application.Groups.Queries.GetGroupsWithPagination
{
    public class GetGroupsWithPaginationQuery : IRequest<PaginatedList<GroupDto>>
    {
        public long? Id { get; set; }

        public long? ChargeStationId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public class GetGroupsWithPaginationQueryHandler :
            IRequestHandler<GetGroupsWithPaginationQuery, PaginatedList<GroupDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetGroupsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PaginatedList<GroupDto>> Handle(GetGroupsWithPaginationQuery request,
                CancellationToken cancellationToken)
            {
                var query = _context.Groups
                    .Include(g => g.ChargeStations)
                    .ThenInclude<Group, ChargeStation, List<Connector>>(s => s.Connectors)
                    .AsQueryable();

                if (request.Id.HasValue) query = query.Where(x => x.Id == request.Id);

                if (request.ChargeStationId.HasValue)
                    query = query.Where(x => x.ChargeStations.Any(s => s.Id == request.ChargeStationId));

                return await query
                    .OrderBy(x => x.Id)
                    .ProjectTo<GroupDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}