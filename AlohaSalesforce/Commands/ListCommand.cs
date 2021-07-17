using AlohaSalesforce.Entities;
using System.Linq;
using System.Text;

namespace AlohaSalesforce.Commands
{
    public class ListCommand : Command
    {
        public string Execute(string[] args)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Join(" ", args));
            foreach (var component in Component.knownComponents.Values.OrderBy(c => c.Name))
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
