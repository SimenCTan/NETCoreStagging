using System;
using System.Data;
using System.Threading.Tasks;

namespace KYExpress.Core.Domain.Uow
{
    /// <summary>
    /// 定义工作单元用来管理事务
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel"></param>
        void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);

        /// <summary>
        /// 事务提交
        /// </summary>
        void Complete();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        Task CompleteAsync();

    }
}
