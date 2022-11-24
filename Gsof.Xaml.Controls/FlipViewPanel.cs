using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Gsof.Xaml.Controls
{
    public class FlipViewPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                if (child == null) { continue; }
                child.Measure(availableSize);
            }

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return DesiredSize;
            }

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                double top = Canvas.GetTop(child);
                double left = Canvas.GetLeft(child);

                left = double.IsNaN(left) ? 0.0 : left;
                top = double.IsNaN(top) ? 0.0 : top;

                child.Arrange(new Rect(left, top, finalSize.Width, finalSize.Height));
            }
            return finalSize;
        }
    }
}
