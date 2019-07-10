using KYExpress.Core.Domain.Query;
using System;
using System.Data;

namespace KYExpress.Dapper.Domain
{
    /// <summary>
    /// 创建连接工厂提供连接
    /// </summary>
    /// <typeparam name="TDbConnection"></typeparam>
    public class DapperDbConnectionFactory<TDbConnection> : IDbConnectionFactory where TDbConnection :class , IDbConnection
    {
        public IDbConnection CreateDbConnection(string connectionString)
        {
            return (IDbConnection)Activator.CreateInstance(typeof(TDbConnection), connectionString);
        }
    }
}
