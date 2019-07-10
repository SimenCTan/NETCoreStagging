using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KYExpress.Core.EventBus.ServiceBus.Mediator.Internal
{

    internal abstract class NotificationHandlerWrapper
    {
        public abstract Task Handle(INotification notification, CancellationToken cancellationToken, ServiceFactory serviceFactory, Func<IEnumerable<Task>, Task> publish);
    }

    internal class NotificationHandlerWrapperImpl<TNotification> : NotificationHandlerWrapper
        where TNotification : INotification
    {
        public override Task Handle(INotification notification, CancellationToken cancellationToken, ServiceFactory serviceFactory, Func<IEnumerable<Task>, Task> publish)
        {
            var handlers = serviceFactory
                .GetInstances<INotificationHandler<TNotification>>()
                .Select(x => x.Handle((TNotification)notification, cancellationToken));

            return publish(handlers);
        }
    }
}