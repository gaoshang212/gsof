using System;
using System.Collections.Generic;
using System.Windows;

namespace Gsof.Xaml.Theme
{
    public class ThemeGroup
    {
        public string Name { get; private set; }

        public List<ResourceDictionary> ResourceDictionaries { get; private set; }

        public ThemeGroup(string p_name, IEnumerable<ResourceDictionary> p_resourceDictionaries)
        {
            if (p_name == null) throw new ArgumentNullException(nameof(p_name));
            if (p_resourceDictionaries == null) throw new ArgumentNullException(nameof(p_resourceDictionaries));

            Name = p_name;
            ResourceDictionaries = new List<ResourceDictionary>(p_resourceDictionaries);
        }
    }
}
