using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows;

namespace Gsof.Xaml.Theme
{
    public class ThemeManager
    {
        private Dictionary<string, ThemeGroup> _currentGroups = new Dictionary<string, ThemeGroup>();

        public ThemeManager()
        {

        }

        public void ApplyResource(ResourceDictionary resources, ThemeGroup p_group)
        {
            if (p_group == null) throw new ArgumentNullException(nameof(p_group));

            var name = p_group.Name;
            ThemeGroup old = null;
            _currentGroups.TryGetValue(name, out old);
            _currentGroups[name] = p_group;
            ChangeAppStyle(resources, old, p_group);
        }

        public void ApplyDefaultResource(ResourceDictionary resources, string p_key, string[] p_sources)
        {
            if (p_key == null) throw new ArgumentNullException(nameof(p_key));

            if (_currentGroups.ContainsKey(p_key))
            {
                return;
            }

            var themeGroup = FindThemeGroup(resources, p_key, p_sources);
            _currentGroups[p_key] = themeGroup;
        }

        public ThemeGroup FindThemeGroup(ResourceDictionary resources, string p_key, string[] p_sources)
        {
            if (p_key == null) throw new ArgumentNullException(nameof(p_key));

            ThemeGroup themeGroup;
            if (!_currentGroups.TryGetValue(p_key, out themeGroup))
            {
                var query = resources.MergedDictionaries.Where(i => p_sources.Any(j => string.Equals(j, i.Source.ToString(), StringComparison.OrdinalIgnoreCase)));

                themeGroup = new ThemeGroup(p_key, query);
            }

            return themeGroup;
        }

        [SecurityCritical]
        private static void ChangeAppStyle(ResourceDictionary resources, ThemeGroup p_oldGroup, ThemeGroup p_newGroup)
        {
            var themeChanged = false;
            if (p_oldGroup != null)
            {
                var oldResource = p_oldGroup.ResourceDictionaries;
                if (oldResource != null && p_oldGroup.Name == p_newGroup.Name)
                {
                    foreach (var dr in oldResource)
                    {
                        themeChanged = resources.MergedDictionaries.Remove(dr);
                    }
                }
            }

            var newResource = p_newGroup.ResourceDictionaries;

            if (newResource != null)
            {
                foreach (var dr in newResource)
                {
                    resources.MergedDictionaries.Add(dr);
                }
            }

            if (themeChanged)
            {
                //OnThemeChanged(newAccent, newTheme);
            }
        }
    }
}
