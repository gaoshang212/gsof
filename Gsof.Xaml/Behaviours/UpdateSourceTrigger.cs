using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Gsof.Extensions;
using Gsof.Xaml.Extensions;
using Microsoft.Xaml.Behaviors;

namespace Gsof.Xaml.Behaviours
{
    public class UpdateSourceTrigger : TriggerAction<UIElement>
    {
        public DependencyObject Target
        {
            get { return (DependencyObject)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(DependencyObject), typeof(UpdateSourceTrigger), new PropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            if (Target == null)
            {
                return;
            }

            var tBindings = Target.GetBindingExpressions();
            tBindings.Apply(i => i.UpdateSource());

            var children = Target.LogicalChildrenOfType<FrameworkElement>(); // LogicalTreeHelper.GetChildren(Target);

            if (children == null)
            {
                return;
            }

            foreach (var child in children)
            {
                var bindings = child.GetBindingExpressions();
                bindings.Apply(i => i.UpdateSource());
            }
        }
    }
}
