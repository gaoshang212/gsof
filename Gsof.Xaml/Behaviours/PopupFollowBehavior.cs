using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Gsof.Xaml.Behaviours
{
    public class PopupFollowBehavior : Behavior<Popup>
    {
        private Window _window;

        protected override void OnAttached()
        {
            base.OnAttached();

            _window = Window.GetWindow(AssociatedObject);
            AssociatedObject.Opened += OnOpened;
            AssociatedObject.Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            if (_window == null)
            {
                return;
            }

            _window.LocationChanged -= OnWindowLocationChanged;
            _window.SizeChanged -= OnWindowSizeChanged;

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Opened -= OnOpened;
            AssociatedObject.Closed -= OnClosed;
        }

        private void OnOpened(object sender, EventArgs e)
        {
            if (_window == null)
            {
                return;
            }

            _window.LocationChanged += OnWindowLocationChanged;
            _window.SizeChanged += OnWindowSizeChanged;
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshOffset();
        }

        private void OnWindowLocationChanged(object sender, EventArgs e)
        {
            RefreshOffset();
        }

        private void RefreshOffset()
        {
            var popup = AssociatedObject;

            var offset = popup.HorizontalOffset;
            popup.HorizontalOffset = offset + 1;
            popup.HorizontalOffset = offset;
        }
    }
}
