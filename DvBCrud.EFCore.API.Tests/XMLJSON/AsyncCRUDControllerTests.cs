﻿using DvBCrud.EFCore.API.Mocks.XMLJSON;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.XMLJSON
{
    public class AsyncCRUDControllerTests
    {
        [Fact]
        public async Task Create_AnyEntity_RepositoryCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyAsyncCRUDController(repo, logger);
            var entity = new AnyEntity();

            // Act
            var result = await controller.Create(entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Create(entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Create_AnyEntityWithId_Returns400BadRequest()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyAsyncCRUDController(repo, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            var result = await controller.Create(entity) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            A.CallTo(() => repo.Create(entity)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Update_AnyEntity_RepositoryUpdatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyAsyncCRUDController(repo, logger);
            var entity = new AnyEntity();

            // Act
            var result = await controller.Update(entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.UpdateAsync(entity, false)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_AnyEntityWithCreate_RepositoryUpdatesOrCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyAsyncCRUDController(repo, logger);
            var entity = new AnyEntity();

            // Act
            var result = await controller.Update(entity, true) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.UpdateAsync(entity, true)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_AnyEntityWithCreate_RepositoryDeletesOrCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyAsyncCRUDController(repo, logger);
            int id = 1;

            // Act
            var result = await controller.Delete(id) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Delete(id)).MustHaveHappenedOnceExactly();
        }
    }
}