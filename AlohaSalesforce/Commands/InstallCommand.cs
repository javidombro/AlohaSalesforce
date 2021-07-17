using AlohaSalesforce.Entities;
using System;
using System.Text;

namespace AlohaSalesforce.Commands
{
    public class InstallCommand : Command
    {
        public string Execute(string[] args)
        {
            var componentName = args[1];
            if (componentName.Length > 10)
            {
                throw new ArgumentOutOfRangeException("Name longer than expected");
            }
            var component = Component.GetComponent(componentName);
            return Install(component);
        }

        private string Install(Component component)
        {
            StringBuilder builder = new StringBuilder();
            if (component.IsInstalled)
            {
                return $"{component.Name} is already installed\n";
            }
            foreach (var dependency in component.Dependencies)
            {
                builder.Append(Install(dependency));
            }
            component.IsInstalled = true;
            builder.AppendLine($"Installing {component.Name}");
            return builder.ToString();
        }
    }
}
