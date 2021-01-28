﻿using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.Tests.Repositories
{
    public class AuditedRepositoryTests
    {
        private readonly ILogger logger;

        public AuditedRepositoryTests()
        {
            logger = A.Fake<ILogger>();
        }

        [Fact]
        public void Create_AnyAuditedEntity_EntityCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_AnyAuditedEntity_EntityCreated));
            var repository = new AnyAuditedRepository(dbContextProvider.DbContext, logger);
            var entityToCreate = new AnyAuditedEntity
            {
                AnyString = "AnyString"
            };
            var expected = new AnyAuditedEntity
            {
                AnyString = "AnyString",
                CreatedBy = 1
            };
            var expectedTime = DateTime.UtcNow;

            repository.Create(entityToCreate, 1);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyAuditedEntities.Single();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.Id).Excluding(x => x.CreatedAt));
            actual.CreatedAt.Should().BeCloseTo(expectedTime);
        }

        [Fact]
        public void Update_AnyAuditedEntity_EntityUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_AnyAuditedEntity_EntityUpdated));
            var repository = new AnyAuditedRepository(dbContextProvider.DbContext, logger);
            var createdAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 12:00:00");
            dbContextProvider.Mock(new[]
            {
                new AnyAuditedEntity
                {
                    Id = 1,
                    AnyString = "AnyString",
                    CreatedBy = 1,
                    CreatedAt = createdAt
                }
            });
            var expectedTime = DateTime.UtcNow;
            var expected = new AnyAuditedEntity
            {
                Id = 1,
                AnyString = "AnyNewString",
                CreatedBy = 1,
                CreatedAt = createdAt,
                UpdatedBy = 1
            };

            repository.Update(1, expected, 1);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyAuditedEntities.Single();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.UpdatedAt));
            actual.UpdatedAt.Should().BeCloseTo(expectedTime);
        }

        [Fact]
        public async Task UpdateAsync_AnyAuditedEntity_EntityUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateAsync_AnyAuditedEntity_EntityUpdated));
            var repository = new AnyAuditedRepository(dbContextProvider.DbContext, logger);
            var createdAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 12:00:00");
            dbContextProvider.Mock(new[]
            {
                new AnyAuditedEntity
                {
                    Id = 1,
                    AnyString = "AnyString",
                    CreatedBy = 1,
                    CreatedAt = createdAt
                }
            });
            var expectedTime = DateTime.UtcNow;
            var expected = new AnyAuditedEntity
            {
                Id = 1,
                AnyString = "AnyNewString",
                CreatedBy = 1,
                CreatedAt = createdAt,
                UpdatedBy = 1
            };

            await repository.UpdateAsync(1, expected, 1);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyAuditedEntities.Single();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.UpdatedAt));
            actual.UpdatedAt.Should().BeCloseTo(expectedTime);
        }


        [Fact]
        public void InheritedCreate_Any_ThrowsNotSupportedException()
        {
            // Arrange
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateAsync_AnyAuditedEntity_EntityUpdated));
            var logger = A.Fake<ILogger>();
            var repo = new AnyAuditedRepository(dbContextProvider.DbContext, logger);

            // Act and Assert
            repo.Invoking(r => r.Create(new AnyAuditedEntity())).Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void InheritedUpdate_Any_ThrowsNotSupportedException()
        {
            // Arrange
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateAsync_AnyAuditedEntity_EntityUpdated));
            var logger = A.Fake<ILogger>();
            var repo = new AnyAuditedRepository(dbContextProvider.DbContext, logger);

            // Act and Assert
            repo.Invoking(r => r.Update(1, new AnyAuditedEntity())).Should().Throw<NotSupportedException>();
        }

        [Fact]
        public async Task InheritedUpdateAsync_Any_ThrowsNotSupportedException()
        {
            // Arrange
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateAsync_AnyAuditedEntity_EntityUpdated));
            var logger = A.Fake<ILogger>();
            var repo = new AnyAuditedRepository(dbContextProvider.DbContext, logger);

            // Act and Assert
            await repo.Awaiting(r => r.UpdateAsync(1, new AnyAuditedEntity())).Should().ThrowAsync<NotSupportedException>();
        }
    }
}
