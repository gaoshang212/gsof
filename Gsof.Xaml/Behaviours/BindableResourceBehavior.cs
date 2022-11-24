using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Gsof.Xaml.Behaviours
{
    public class BindableResourceBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// Resource x:Key Name 
        /// </summary>
        public string ResourceName
        {
            get { return (string)GetValue(ResourceNameProperty); }
            set { SetValue(ResourceNameProperty, value); }
        }

        public static readonly DependencyProperty ResourceNameProperty =
            DependencyProperty.Register("ResourceName", typeof(string), typeof(BindableResourceBehavior), new PropertyMetadata(default(string), OnResourceNameChanged));

        /// <summary>
        /// Bindable DependencyProperty 
        /// </summary>
        public DependencyProperty Property
        {
            get { return (DependencyProperty)GetValue(PropertyProperty); }
            set { SetValue(PropertyProperty, value); }
        }

        public static readonly DependencyProperty PropertyProperty =
            DependencyProperty.Register("Property", typeof(DependencyProperty), typeof(BindableResourceBehavior), new PropertyMetadata(default(DependencyProperty), OnPropertyChanged));

        protected override void OnAttached()
        {
            AssociatedObject.SetResourceReference(Property, ResourceName);
            base.OnAttached();
        }

        protected virtual void SetResourceReference(DependencyProperty p_pd, string p_resourceName)
        {
            AssociatedObject.SetResourceReference(p_pd, p_resourceName);
        }

        private static void OnResourceNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as BindableResourceBehavior;
            if (behavior == null)
            {
                return;
            }

            behavior.SetResourceReference(behavior.Property, e.NewValue as string);
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as BindableResourceBehavior;
            if (behavior == null)
            {
                return;
            }

            behavior.SetResourceReference((DependencyProperty)e.NewValue, behavior.ResourceName);
        }
    }
}
