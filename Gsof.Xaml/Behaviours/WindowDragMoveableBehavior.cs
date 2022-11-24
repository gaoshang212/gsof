using System.Windows;
using System.Windows.Input;
using Gsof.Xaml.Extensions;
using Microsoft.Xaml.Behaviors;

namespace Gsof.Xaml.Behaviours
{
    public class WindowDragMoveableBehavior : Behavior<FrameworkElement>
    {
        public bool AllowHandler
        {
            get { return (bool)GetValue(AllowHandlerProperty); }
            set { SetValue(AllowHandlerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowHandler.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowHandlerProperty =
            DependencyProperty.Register("AllowHandler", typeof(bool), typeof(WindowDragMoveableBehavior), new PropertyMetadata(false));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), AllowHandler);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown));
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnWindowDragMove();
        }

        private void OnWindowDragMove()
        {
            var window = AssociatedObject.GetWindow();
            if (window == null)
            {
                return;
            }

            window.DragMove();
        }
    }
}
