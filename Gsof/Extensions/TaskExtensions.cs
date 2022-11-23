using System.Threading.Tasks;

namespace Gsof.Extensions
{
    public static class TaskExtensions
    {
        public static Task CompletedTask = FromResult(false);

        public static Task<TResult> FromResult<TResult>(TResult p_result)
        {
#if NET40
            return new Task<TResult>(() => p_result);
#else
            return Task.FromResult(p_result);
#endif
        }
    }
}
