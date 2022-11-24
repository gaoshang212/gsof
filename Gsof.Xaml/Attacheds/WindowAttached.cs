using System.Windows;
using Gsof.Xaml.Behaviours;
using Gsof.Xaml.Extensions;

namespace Gsof.Xaml.Attacheds
{
    public class WindowAttached
    {
        public static bool GetMinimize(DependencyObject obj)
        {
            return (bool)obj.GetValue(MinimizeProperty);
        }

        public static void SetMinimize(DependencyObject obj, bool value)
        {
            obj.SetValue(MinimizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Minimized.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimizeProperty =
            DependencyProperty.RegisterAttached("Minimize", typeof(bool), typeof(WindowAttached), new PropertyMetadata(false,
                (o, args) =>
                {
                    o.ApplyBehavior<WindowMinimizedBehavior>(!(bool)args.NewValue);
                }));

        public static bool GetMaximize(DependencyObject obj)
        {
            return (bool)obj.GetValue(MaximizeProperty);
        }

        public static void SetMaximize(DependencyObject obj, bool value)
        {
            obj.SetValue(MaximizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Maximized.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximizeProperty =
            DependencyProperty.RegisterAttached("Maximize", typeof(bool), typeof(WindowAttached), new PropertyMetadata(false, (o, args) =>
            {
                o.ApplyBehavior<WindowMaximizedBehavior>(!(bool)args.NewValue);
            }));

        public static bool GetCloseable(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseableProperty);
        }

        public static void SetCloseable(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseableProperty, value);
        }

        // Using a DependencyProperty as the backing store for Closed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseableProperty =
            DependencyProperty.RegisterAttached("Closeable", typeof(bool), typeof(WindowAttached), new PropertyMetadata(false,
                (o, args) =>
                {
                    o.ApplyBehavior<WindowClosedBehavior>(!(bool)args.NewValue);
                }));



        public static bool GetDragMoveable(DependencyObject obj)
        {
            return (bool)obj.GetValue(DragMoveableProperty);
        }

        public static void SetDragMoveable(DependencyObject obj, bool value)
        {
            obj.SetValue(DragMoveableProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragMoveable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragMoveableProperty =
            DependencyProperty.RegisterAttached("DragMoveable", typeof(bool), typeof(WindowAttached), new PropertyMetadata(false, (o, args) =>
                 {
                     o.ApplyBehavior<WindowDragMoveableBehavior>(!(bool)args.NewValue);
                 }));


    }
}
