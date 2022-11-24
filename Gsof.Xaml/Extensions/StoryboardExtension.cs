using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Gsof.Extensions;

namespace Gsof.Xaml.Extensions
{
    public static class StoryboardExtension
    {
        public static Task BeginAsync(this Storyboard p_storyboard)
        {
            var sb = p_storyboard;
            if (sb == null)
            {
                return Gsof.Extensions.TaskExtensions.FromResult(string.Empty);
            }

            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            sb.Completed += (sender, args) =>
            {
                tcs.SetResult("");
            };

            sb.Begin();

            return tcs.Task;
        }
    }
}
