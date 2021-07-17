using AlohaSalesforce.Entities;
using System;
using System.Linq;
using System.Text;

namespace AlohaSalesforce.Commands
{
    /// <summary>
    /// Implements the REMOVE command
    /// </summary>
    public class RemoveCommand : Command
    {
        public string Execute(string[] args)
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Join(" ", args));
            var componentName = args[1];
            if (componentName.Length > 10)
            {
                throw new ArgumentException("Name longer than expected");
            }
            var component = Component.GetComponent(componentName);

            var output = component.IsInstalled ? Remove(component) : $"{component.Name} is not installed";
            builder.Append(output);
            return builder.ToString();
        }

        /// <summary>
        /// Removes the given component and it's dependencies whenever is possible.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
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
                if (!dependency.ExplicityInstalled && !dependency.Dependents.Any(c => c.IsInstalled))
                {
                    builder.AppendLine(Remove(dependency));
                }
            }
            return builder.ToString();
        }
    }
}
