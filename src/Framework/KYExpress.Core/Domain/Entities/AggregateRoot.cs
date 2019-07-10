using KYExpress.Core.EventBus;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KYExpress.Core.Domain.Entities
{
    /// <summary>
    /// DDD聚合根
    /// </summary>
    public abstract class AggregateRoot
    {
        public AggregateRoot()
        {
            DomainEvents = new Collection<INotification>();
        }

        [NotMapped]
        public ICollection<INotification> DomainEvents { get; }

    }
}
