namespace Domain.Pipeline
{
    using System.Collections.Generic;
    using System.Linq;
    using MediatR;

    public class CommandResult
    {
        public static CommandResult Void => new CommandResult();

        CommandResult()
        {
        }

        public IEnumerable<INotification> GetNotifications()
        {
            return Enumerable.Empty<INotification>();
        }
    }
}