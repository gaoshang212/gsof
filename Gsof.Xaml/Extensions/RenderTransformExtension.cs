using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Gsof.Xaml.Extensions
{
    public static class RenderTransformExtension
    {
        public static T GetRenderTransform<T>(this UIElement p_element) where T : Transform, new()
        {
            var element = p_element;

            if (Equals(element.RenderTransform, Transform.Identity))
            {
                element.CreateRenderTransform();
            }

            T t;
            do
            {
                t = element.RenderTransform as T;
                if (t != null)
                {
                    break;
                }

                var group = element.RenderTransform as TransformGroup;
                if (group != null)
                {
                    t = group.Children.FirstOrDefault(i => i is T) as T;
                    break;
                }

                var tg = new TransformGroup();
                tg.Children.Add(t);
                element.RenderTransform = tg;

            } while (false);

            return t;
        }

        public static void CreateRenderTransform(this UIElement p_element)
        {
            var element = p_element;
            if (element == null)
            {
                return;
            }

            element.RenderTransform = new TransformGroup()
            {
                Children = new TransformCollection()
                        {
                            new ScaleTransform(),
                            new SkewTransform(),
                            new RotateTransform(),
                            new TranslateTransform(),
                        }
            };
        }
    }
}
