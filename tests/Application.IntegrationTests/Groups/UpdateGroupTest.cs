using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Groups.Commands.UpdateGroup;
using GreenFlux.Domain.Entities;
using GreenFlux.Domain.Exceptions;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.Groups
{
    using static Testing;

    public class UpdateGroupTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UpdateGroupCommand
            {
                Capacity = 0
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldNotUpdateIfTotalCurrentExceedsNewCapacity()
        {
            var group = await AddGroupAsync();
            var chargeStation = await AddChargeStationAsync(group.Id);
            await AddConnectorAsync(chargeStation.Id, 1, 30);

            var command = new UpdateGroupCommand
            {
                GroupId = group.Id,
                Capacity = 10,
                Name = "Den Hague"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityUpdateException>();

            var list = await FindAsync<Group>(group.Id);

            list.Should().NotBeNull();
            list.Name.Should().Be(group.Name);
            list.Capacity.Should().Be(group.Capacity);
        }

        [Test]
        public async Task ShouldUpdateGroup()
        {
            var group = await AddGroupAsync();
            var command = new UpdateGroupCommand
            {
                GroupId = group.Id,
                Capacity = 10,
                Name = "Den Hague"
            };

            await SendAsync(command);

            var list = await FindAsync<Group>(group.Id);

            list.Should().NotBeNull();
            list.Name.Should().Be(command.Name);
            list.Capacity.Should().Be(command.Capacity);
        }
    }
}