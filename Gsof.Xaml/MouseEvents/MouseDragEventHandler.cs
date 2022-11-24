using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Gsof.Xaml.Extensions;

namespace Gsof.Xaml.MouseEvents
{
    public class MouseDragEventHandler
    {
        private UIElement _element;
        private bool _isLeftButtonDown;
        private Point? _startPoint;


        public event EventHandler<MouseDragEventArgs> MouseDrag;

        public MouseDragEventHandler()
        {
            
        }

        private void InitEvents(UIElement p_element, bool p_handler = false)
        {
            var element = p_element;
            if (element == null)
            {
                return;
            }

            element.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), p_handler);
            element.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp), p_handler);
            element.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(OnMouseMove), p_handler);
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _startPoint = null;
            _isLeftButtonDown = false;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as UIElement;
            if (element == null)
            {
                return;
            }

            _isLeftButtonDown = true;
            _startPoint = e.GetPosition(element);
            e.Handled = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed
                || !_isLeftButtonDown
                || _startPoint == null)
            {
                return;
            }

            var element = sender as UIElement;
            if (element == null)
            {
                return;
            }

            if (element.IsMouseCaptured)
            {
                element = element.ParentOfType<UIElement>();
            }

            var cPoint = e.GetPosition(element);
            var x = cPoint.X - _startPoint.Value.X;
            var y = cPoint.Y - _startPoint.Value.Y;


            e.Handled = true;
        }
    }
}
