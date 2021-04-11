using MediatR;

namespace GreenFlux.Application.Groups.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<long>
    {
        public string Name { get; set; }
        public float Capacity { get; set; }
    }
}