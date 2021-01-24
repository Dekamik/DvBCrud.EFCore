﻿using DvBCrud.EFCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.JSON
{
    public interface ICRUDController<TId, TEntity>
        where TEntity : BaseEntity<TId>
    {
        [HttpPost]
        Task<IActionResult> Create([FromBody]IEnumerable<TEntity> entity);

        [HttpPut]
        Task<IActionResult> Update([FromBody]IEnumerable<TEntity> entity, [FromQuery] bool createIfNotExists = false);

        [HttpDelete, Route("{id}")]
        Task<IActionResult> Delete(TId id);

        [HttpDelete]
        Task<IActionResult> Delete([FromBody]IEnumerable<TId> id);
    }
}