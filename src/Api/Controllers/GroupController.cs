using System.Threading.Tasks;
using GreenFlux.Application.Common.Models;
using GreenFlux.Application.Dto.Queries;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.DeleteGroup;
using GreenFlux.Application.Groups.Commands.UpdateGroup;
using GreenFlux.Application.Groups.Queries.GetGroupsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux.Api.Controllers
{
    public class GroupController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateGroupCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Update(UpdateGroupCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<GroupDto>>> GetGroupsWithPagination(
            [FromQuery] GetGroupsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteGroupCommand {GroupId = id});

            return NoContent();
        }
    }
}