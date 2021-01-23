﻿using DvBCrud.EFCore.Tests.Mocks.Entities;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore.Tests.Mocks.DbContexts
{
    public class AnyDbContext : DbContext
    {
        public AnyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AnyEntity> AnyEntities { get; set; }
    }
}
