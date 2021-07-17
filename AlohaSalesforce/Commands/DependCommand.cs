using AlohaSalesforce.Entities;
using System.Collections.Generic;

namespace AlohaSalesforce.Commands
{
    class DependCommand : Command
    {
        public string Execute(string[] args)
        {
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
                    return $"{dependency.Name} depends on {component.Name}, ignoring command";
                }
            }
            // 3. Add dependencies
            AddDependencies(component, newDependencies);
            return string.Join(" ", args);
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
