using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gsof.Xaml.Extensions
{
    public static class UIElementExtension
    {
        /// <summary>
        /// 延迟获得焦点，默认延迟 100ms
        /// </summary>
        /// <param name="p_element"></param>
        /// <param name="p_time"></param>
        public static void FocusDelay(this UIElement p_element, int p_time = 100)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(p_time);
                p_element.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => p_element.Focus()));
            });
        }

        /// <summary>
        /// 获取UIElement的相对位置
        /// </summary>
        /// <param name="source"></param>
        /// <param name="p_relative">相对于UIElement</param>
        /// <returns>坐标值</returns>
        public static Point GetRelativePosition(this UIElement source, UIElement p_relative)
        {
            Point pt = new Point();
            MatrixTransform mat = source.TransformToVisual(p_relative) as MatrixTransform;
            if (mat != null)
            {
                pt.X = mat.Matrix.OffsetX;
                pt.Y = mat.Matrix.OffsetY;
            }
            return pt;
        }
    }
}
