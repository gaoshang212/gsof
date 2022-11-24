using System.Windows;
using System.Windows.Controls;

namespace Gsof.Xaml.Controls
{
    public class FlipViewItem : ContentControl
    {
        static FlipViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipViewItem), new FrameworkPropertyMetadata(typeof(FlipViewItem)));

            HorizontalContentAlignmentProperty.OverrideMetadata(typeof(FlipViewItem), new FrameworkPropertyMetadata(HorizontalAlignment.Stretch));
            VerticalContentAlignmentProperty.OverrideMetadata(typeof(FlipViewItem), new FrameworkPropertyMetadata(VerticalAlignment.Stretch));
        }
    }
}