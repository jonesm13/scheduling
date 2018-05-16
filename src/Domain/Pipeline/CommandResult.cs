namespace Domain.Pipeline
{
    using System.Collections.Generic;
    using MediatR;

    public class CommandResult
    {
        public static CommandResult Void => new CommandResult();

        readonly List<INotification> notifications;

        CommandResult()
        {
            notifications = new List<INotification>();
        }

        public IEnumerable<INotification> GetNotifications()
        {
            return notifications.AsReadOnly();
        }

        public CommandResult WithNotification(INotification notification)
        {
            notifications.Add(notification);
            return this;
        }
    }
}