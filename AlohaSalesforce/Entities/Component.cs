using System.Collections.Generic;

namespace AlohaSalesforce.Entities
{
    public class Component
    {
        public static Dictionary<string, Component> knownComponents = new Dictionary<string, Component>();
        public string Name { get; set; }
        public ICollection<Component> Dependencies { get; set; } = new List<Component>();
        public ICollection<Component> Dependents { get; set; } = new List<Component>();
        public bool IsInstalled { get; set; } = false;
        public bool ExplicityInstalled { get; set; } = false;

        private Component(string name)
        {
            Name = name;
        }
        internal static Component GetComponent(string componentName)
        {
            var component = knownComponents.GetValueOrDefault(componentName);
            if (component is null)
            {
                component = new Component(componentName);
                knownComponents.Add(componentName, component);
            }
            return component;
        }

        internal bool DependsOn(Component component)
        {
            //var result = false;
            if (Dependencies.Contains(component))
            {
                return true;
            }
            foreach (var dependency in Dependencies)
            {
                if (dependency.DependsOn(component))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
