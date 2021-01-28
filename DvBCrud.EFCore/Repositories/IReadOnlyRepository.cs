﻿using DvBCrud.EFCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    /// <summary>
    /// A read-only repository for querying untracked <typeparamref name="TEntity"/> instances in database
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId"><typeparamref name="TEntity"/> key type</typeparam>
    public interface IReadOnlyRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>An <see cref="IQueryable"/> containing all <typeparamref name="TEntity"/> instances</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Finds a single <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/>
        /// </summary>
        /// <param name="id">Key value to query</param>
        /// <returns>The matching <typeparamref name="TEntity"/></returns>
        TEntity Get(TId id);

        /// <summary>
        /// Finds a single <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/> asynchronously
        /// </summary>
        /// <param name="id">Key value to query</param>
        /// <returns>The matching <typeparamref name="TEntity"/></returns>
        Task<TEntity> GetAsync(TId id);
    }
}
