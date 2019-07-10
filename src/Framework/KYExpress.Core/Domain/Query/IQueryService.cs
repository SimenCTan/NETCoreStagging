using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYExpress.Core.Domain.Query
{
    /// <summary>
    /// 查询服务接口
    /// </summary>
    public interface IQueryService
    {
        /// <summary>
        /// 查询服务切换数据库
        /// </summary>
        /// <param name="connectionString"></param>
        void ChangeDataBase(string connectionString);

        /// <summary>
        /// 同步查询语句返回任意实体实体集
        /// </summary>
        /// <typeparam name="TAny"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<TAny> Query<TAny>(string query,  object parameters = null) where TAny : class;

        /// <summary>
        /// 同步查询语句返回任意实体
        /// </summary>
        /// <typeparam name="TAny"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        TAny QueryFirst<TAny>(string query, object parameters = null) where TAny : class;

        /// <summary>
        /// 异步查询语句返回任意实体集
        /// </summary>
        /// <typeparam name="TAny"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<TAny>> QueryAsync<TAny>(string query,object parameters = null) where TAny : class;

        /// <summary>
        /// 异步查询语句返回任意实体
        /// </summary>
        /// <typeparam name="TAny"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<TAny> QueryFirstAsync<TAny>(string query, object parameters = null) where TAny : class;
    }
}
