﻿using Conversion.Domain.Entities;

namespace Conversion.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task Update(TEntity obj);

        Task Delete(int id);

        Task<IList<TEntity>> Select();

        Task<TEntity> Select(int id);

    }
}