using MediatR;

namespace Todo.Domain.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private readonly List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual List<DomainNotification> GetErrors()
        {
            return _notifications.Where(x => x.Type == NotificationType.Error).ToList();
        }

        public virtual List<DomainNotification> GetInformations()
        {
            return _notifications.Where(x => x.Type == NotificationType.Information).ToList();
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public virtual bool HasError()
        {
            return GetNotifications().Any(x => x.Type == NotificationType.Error);
        }

        public virtual bool HasInformation()
        {
            return GetNotifications().Any(x => x.Type == NotificationType.Information);
        }
    }
}
