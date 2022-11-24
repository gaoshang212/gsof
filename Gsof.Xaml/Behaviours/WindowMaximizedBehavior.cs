using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Gsof.Xaml.Behaviours
{
    public class WindowMaximizedBehavior : Behavior<ButtonBase>
    {
        private Window _window;

        protected override void OnAttached()
        {
            base.OnAttached();

            var window = GetWindow();
            if (window != null)
            {
                window.StateChanged += OnWindowState;
            }

            AssociatedObject.Click += OnButtonClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            var window = GetWindow();
            if (window != null)
            {
                window.StateChanged -= OnWindowState;
            }
            AssociatedObject.Click -= OnButtonClick;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var window = GetWindow();
            if (window == null)
            {
                return;
            }

            window.WindowState = UpdateButtonChecked() ? WindowState.Normal : WindowState.Maximized;
        }

        private bool UpdateButtonChecked()
        {
            var window = GetWindow();
            var isMax = window != null && window.WindowState == WindowState.Maximized;

            if (AssociatedObject is ToggleButton)
            {
                ToggleButton tb = (ToggleButton)AssociatedObject;
                tb.IsChecked = isMax;
            }

            return isMax;
        }

        private void OnWindowState(object? sender, EventArgs e)
        {
            UpdateButtonChecked();
        }

        private Window GetWindow()
        {
            if (_window == null)
            {
                _window = Window.GetWindow(AssociatedObject);
            }

            return _window;
        }
    }
}