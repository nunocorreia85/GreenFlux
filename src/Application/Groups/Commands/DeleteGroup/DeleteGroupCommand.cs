using MediatR;

namespace GreenFlux.Application.Groups.Commands.DeleteGroup
{
    public class DeleteGroupCommand : IRequest
    {
        public long Id { get; set; }
    }
}