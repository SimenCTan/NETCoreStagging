using KYExpress.Core.Domain.Uow;
using KYExpress.Core.EventBus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KYExpress.Dapper.Domain
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IEventBus _eventBus;

        public DapperUnitOfWork(IDbConnection dbConnection, IEventBus eventBus)
        {
            _eventBus = eventBus;
            ActiveConnection = dbConnection;
            DomainEvents = new List<INotification>();
        }

        public IDbConnection ActiveConnection { get; }
        public IDbTransaction ActiveTransaction { get; private set; }

        public List<INotification> DomainEvents { get; }

        public void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            ActiveConnection.Open();
            ActiveTransaction = ActiveConnection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// 发布领域事件
        /// </summary>
        /// <param name="domainEvents"></param>
        public void PublishDomainEvents(ICollection<INotification> domainEvents)
        {
            CancellationToken cancellationToken = new CancellationToken();
            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await _eventBus.Publish(domainEvent, cancellationToken);
                });

            Task.WhenAll(tasks).Wait();
        }

        public void Complete()
        {
            PublishDomainEvents(DomainEvents);
            DomainEvents.Clear();
            ActiveTransaction?.Commit();
        }

        public Task CompleteAsync()
        {
            Complete();
            return Task.FromResult(0);
        }

   

        private bool _disposed = false;
      
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

       
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ActiveConnection.Dispose();
                    ActiveTransaction?.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
