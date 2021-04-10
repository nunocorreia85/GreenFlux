﻿using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.TodoItems.Commands.CreateTodoItem;
using GreenFlux.Application.TodoItems.Commands.DeleteTodoItem;
using GreenFlux.Application.TodoLists.Commands.CreateTodoList;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new DeleteTodoItemCommand {Id = 99};

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoItem()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            await SendAsync(new DeleteTodoItemCommand
            {
                Id = itemId
            });

            var list = await FindAsync<TodoItem>(listId);

            list.Should().BeNull();
        }
    }
}