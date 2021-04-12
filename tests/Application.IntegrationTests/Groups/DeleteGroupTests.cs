using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.DeleteGroup;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.Groups
{
    public class DeleteGroupTests : TestBase
    {
        [Test]
        public void ShouldRequireValidGroupId()
        {
            var command = new DeleteGroupCommand { Id = 99 };

            FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteGroup()
        {
            var command = new CreateGroupCommand()
            {
                Capacity = 100,
                Name = "Amstedam"
            };

            var id = await Testing.SendAsync(command);
            var list = await Testing.FindAsync<Group>(id);
            
            await Testing.SendAsync(new DeleteGroupCommand 
            { 
                Id = id 
            });

            list = await Testing.FindAsync<Group>(id);

            list.Should().BeNull();
        }
    }
}