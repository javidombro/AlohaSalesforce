using AlohaSalesforce.Entities;
using System;
using System.Text;

namespace AlohaSalesforce.Commands
{
    /// <summary>
    /// Implements the INSTALL command.
    /// </summary>
    public class InstallCommand : Command
    {
        public string Execute(string[] args)
        {

            var builder = new StringBuilder();
            builder.AppendLine(string.Join(" ", args));
            var componentName = args[1];
            if (componentName.Length > 10)
            {
                throw new ArgumentOutOfRangeException("Name longer than expected");
            }
            var component = Component.GetComponent(componentName);
            component.ExplicityInstalled = true;
            var output = component.IsInstalled ? $"{component.Name} is already installed" : Install(component);
            builder.Append(output);
            return builder.ToString();
        }

        /// <summary>
        /// Installs the given component and all it's dependencies.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        private string Install(Component component)
        {
            StringBuilder builder = new StringBuilder();
            if (!component.IsInstalled)
            {
                foreach (var dependency in component.Dependencies)
                {
                    builder.Append(Install(dependency));
                }
                component.IsInstalled = true;
                builder.AppendLine($"Installing {component.Name}");
            }
            return builder.ToString();
        }
    }
}
