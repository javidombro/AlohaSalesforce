using AlohaSalesforce.Entities;
using System.Text;

namespace AlohaSalesforce.Commands
{
    public class ListCommand : Command
    {
        public string Execute(string[] args)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var component in Component.knownComponents.Values)
            {
                if (component.IsInstalled)
                {
                    builder.AppendLine(component.Name);
                }
            }
            return builder.ToString();
        }
    }
}
