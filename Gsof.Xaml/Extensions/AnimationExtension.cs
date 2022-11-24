using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Gsof.Xaml.Extensions
{
    public static class AnimationExtension
    {
        public static void To(this UIElement p_element, double p_from, double p_to, Action p_action)
        {
            var element = p_element;
            if (element == null)
            {
                return;
            }

            var tt = element.GetRenderTransform<TranslateTransform>();


            Storyboard story = new Storyboard();
            story.FillBehavior = FillBehavior.Stop;
            Storyboard.SetTargetProperty(story, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            Storyboard.SetTarget(story, element);

            EasingDoubleKeyFrame fromFrame = new EasingDoubleKeyFrame(p_from);
            fromFrame.EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut };
            fromFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));

            EasingDoubleKeyFrame toFrame = new EasingDoubleKeyFrame(p_to);
            toFrame.EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut };
            toFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));

            var doubleAnimation = new DoubleAnimationUsingKeyFrames();

            doubleAnimation.KeyFrames.Add(fromFrame);
            doubleAnimation.KeyFrames.Add(toFrame);

            story.Children.Add(doubleAnimation);

            story.Completed += (s, e) =>
            {
                p_action?.Invoke();
                tt.X = p_to;
            };
            story.Begin();

            //doubleAnimation.Completed += (sender, args) => p_action?.Invoke();
            //tt.BeginAnimation(TranslateTransform.XProperty, doubleAnimation, HandoffBehavior.SnapshotAndReplace);
        }

        public static void To(this UIElement p_element, double p_to, Action p_action)
        {
            var tt = p_element.GetRenderTransform<TranslateTransform>();
            p_element.To(tt.X, p_to, p_action);
        }

        public static Task BeginStoryboard(this FrameworkElement p_fe, string p_key)
        {
            var element = p_fe;
            if (element == null)
            {
                return Gsof.Extensions.TaskExtensions.CompletedTask;
            }

            var sb = element.FindResource(p_key) as Storyboard;
            if (sb != null)
            {
                return sb.BeginAsync();
            }

            return Gsof.Extensions.TaskExtensions.CompletedTask;
        }
    }
}
