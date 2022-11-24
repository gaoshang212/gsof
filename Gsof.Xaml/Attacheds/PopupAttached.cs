using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Gsof.Xaml.Behaviours;
using Gsof.Xaml.Extensions;

namespace Gsof.Xaml.Attacheds
{
    public class PopupAttached
    {
        public static bool GetAllowFollow(DependencyObject obj)
        {
            return (bool)obj.GetValue(AllowFollowProperty);
        }

        public static void SetAllowFollow(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowFollowProperty, value);
        }

        // Using a DependencyProperty as the backing store for AllowFollow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowFollowProperty =
            DependencyProperty.RegisterAttached("AllowFollow", typeof(bool), typeof(PopupAttached), new PropertyMetadata(false,
                (d, e) =>
                {
                    d.ApplyBehavior<PopupFollowBehavior>();
                }));
    }
}
