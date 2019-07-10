using DapperExtensions;
using KYExpress.Core.Domain.Entities;
using KYExpress.Core.Domain.Repositories;
using KYExpress.Core.Domain.Uow;
using KYExpress.Dapper.Expressions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace KYExpress.Dapper.Domain
{
    public class DapperRepository<TEntity> : RepositoryBase<TEntity> where TEntity : class, IEntity
    {
        private readonly DapperUnitOfWork _unitOfWork;

        public virtual DbConnection Connection
        {
            get { return (DbConnection)_unitOfWork.ActiveConnection; }
        }

        public virtual DbTransaction Transaction
        {
            get { return (DbTransaction)_unitOfWork.ActiveTransaction; }
        }

        public DapperRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (DapperUnitOfWork)unitOfWork;
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(predicate).FirstOrDefault();
        }

        public override IEnumerable<TEntity> GetAll()
        {
            return Connection.GetList<TEntity>(transaction: Transaction);
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.GetList<TEntity>(predicate.ToPredicateGroup(), transaction: Transaction);
        }

        public override void Insert(TEntity entity)
        {
            Connection.Insert(entity, transaction: Transaction);
            AddDomainEvents(entity);

        }
        public override void Delete(TEntity entity)
        {
            Connection.Update(entity, transaction: Transaction);
            AddDomainEvents(entity);
        }

        public override void Update(TEntity entity)
        {
            Connection.Update(entity, transaction: Transaction);
            AddDomainEvents(entity);
        }


        private void AddDomainEvents(TEntity entity)
        {
            var aggregateRoot = entity as AggregateRoot;

            if (aggregateRoot != null)
            {
                if (_unitOfWork.ActiveTransaction == null)
                {
                    _unitOfWork.PublishDomainEvents(aggregateRoot.DomainEvents);
                }
                else
                {
                    //事务提交的时候才发布事件通知
                    _unitOfWork.DomainEvents.AddRange(aggregateRoot.DomainEvents);
                }

            }
         
        }
    }
}
