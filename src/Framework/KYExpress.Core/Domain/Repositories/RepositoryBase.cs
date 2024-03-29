﻿using KYExpress.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KYExpress.Core.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {

        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public abstract TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(FirstOrDefault(predicate));
        }

        public abstract IEnumerable<TEntity> GetAll();

        public abstract IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAll(predicate));
        }

        public abstract void Insert(TEntity entity);

        public virtual Task InsertAsync(TEntity entity)
        {
            Insert(entity);
            return Task.FromResult(0);
        }

        public abstract void Update(TEntity entity);

        public virtual Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.FromResult(0);
        }
    }
}
