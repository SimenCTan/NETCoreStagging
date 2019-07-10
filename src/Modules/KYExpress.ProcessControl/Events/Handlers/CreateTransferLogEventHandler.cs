using KYExpress.Core.Domain;
using KYExpress.Core.EventBus;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace KYExpress.ProcessControl.Events.Handlers
{
    public class CreateTransferLogEventHandler : INotificationHandler<CreateTransferLogEvent>
    {
        private readonly IRepository<Domain.TA_PDAScanRecords> _scanRecordsRepository;
        public CreateTransferLogEventHandler(IRepository<Domain.TA_PDAScanRecords> repository)
        {
            _scanRecordsRepository = repository;
        }
        public async Task Handle(CreateTransferLogEvent notification, CancellationToken cancellationToken)
        {
            var scanRecords = new Domain.TA_PDAScanRecords(notification.HONo, notification.HLoad, notification.UpPerson, 
                notification.ScanType, notification.BeforHLoad,DateTime.Now);
            await _scanRecordsRepository.InsertAsync(scanRecords);
        }
    }
}
