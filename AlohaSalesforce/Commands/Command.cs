namespace AlohaSalesforce.Commands
{
    /// <summary>
    /// Interface for the command pattern.
    /// </summary>
    public interface Command
    {
        public string Execute(string[] args);
    }
}
