using System;
using System.Linq.Expressions;
using DapperExtensions;
using KYExpress.Core.Domain.Entities;

namespace KYExpress.Dapper.Expressions
{
    internal static class DapperExpressionExtensions
    {
        public static IPredicate ToPredicateGroup<TEntity>(this Expression<Func<TEntity, bool>> expression) 
            where TEntity : class, IEntity
        {

            var dev = new DapperExpressionVisitor<TEntity>();
            IPredicate pg = dev.Process(expression);
            return pg;
        }
    }
}
