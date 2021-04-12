using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Domain.Entities;
using GreenFlux.Application.Common.Exceptions;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.Groups
{
    using static Testing;

    public class CreateGroupTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateGroupCommand()
            {
                Capacity = 0
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }
        
        [Test]
        public async Task ShouldCreateGroup()
        {
            var command = new CreateGroupCommand()
            {
                Capacity = 100,
                Name = "Amstedam"
            };

            var id = await SendAsync(command);

            var list = await FindAsync<Group>(id);

            list.Should().NotBeNull();
            list.Name.Should().Be(command.Name);
            list.Capacity.Should().Be(command.Capacity);
        }
    }
}