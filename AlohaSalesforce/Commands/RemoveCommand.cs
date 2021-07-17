using AlohaSalesforce.Entities;
using System;
using System.Linq;
using System.Text;

namespace AlohaSalesforce.Commands
{
    class RemoveCommand : Command
    {
        public string Execute(string[] args)
        {
            var componentName = args[1];
            if (componentName.Length > 10)
            {
                throw new ArgumentException("Name longer than expected");
            }
            var component = Component.GetComponent(componentName);
            if (!component.IsInstalled)
            {
                return $"{component.Name} is not installed";
            }
            return Remove(component);
        }

        private string Remove(Component component)
        {
            StringBuilder builder = new StringBuilder();
            if (component.Dependents.Any(c => c.IsInstalled))
            {
                return $"{component.Name} is still needed";
            }

            //Removes the component
            component.IsInstalled = false;
            builder.AppendLine($"Removing {component.Name}");

            //Checks if is possible to remove it's dependencies
            foreach (var dependency in component.Dependencies)
            {
                if (!dependency.Dependents.Any(c => c.IsInstalled))
                {
                    builder.AppendLine(Remove(dependency));
                }
            }
            return builder.ToString();
        }
    }
}
