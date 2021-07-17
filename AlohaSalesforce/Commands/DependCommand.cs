using AlohaSalesforce.Entities;
using System.Collections.Generic;
using System.Text;

namespace AlohaSalesforce.Commands
{
    public class DependCommand : Command
    {
        public string Execute(string[] args)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Join(" ", args));
            var componentName = args[1];
            var component = Component.GetComponent(componentName);

            // 1. Load all new dependencies
            var newDependencies = new List<Component>();
            for (int i = 2; i < args.Length; i++)
            {
                var dependency = Component.GetComponent(args[i]);
                newDependencies.Add(dependency);
            }
            // 2. Check there is no conflict to add them
            foreach (var dependency in newDependencies)
            {
                if (dependency.DependsOn(component))
                {
                    builder.AppendLine();
                    builder.Append($"{dependency.Name} depends on {component.Name}, ignoring command");
                    return builder.ToString();
                }
            }
            // 3. Add dependencies
            AddDependencies(component, newDependencies);
            return builder.ToString();
        }

        private void AddDependencies(Component component, List<Component> newDependencies)
        {
            foreach (var dependency in newDependencies)
            {
                component.Dependencies.Add(dependency);
                dependency.Dependents.Add(component);
            }
        }
    }
}
