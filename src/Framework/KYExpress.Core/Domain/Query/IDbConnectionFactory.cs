using System.Data;

namespace KYExpress.Core.Domain.Query
{
    /// <summary>
    /// 提供查询服务的连接工厂
    /// </summary>
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection(string connectionString);
    }
}
