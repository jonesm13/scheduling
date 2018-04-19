namespace Domain.Pipeline
{
    public class CommandResult
    {
        public static CommandResult Void => new CommandResult();

        CommandResult()
        {
        }
    }
}