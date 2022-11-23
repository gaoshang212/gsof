using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Gsof.Extensions
{
    public static class ProcessExtension
    {
        public static Task<string> Exec(this Process p_process, int timeout = -1)
        {
            var process = p_process;
            if (process == null)
            {
                return TaskExtensions.FromResult(string.Empty);
            }

            var tcs = new TaskCompletionSource<string>();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;

                    process.Start();
                    process.WaitForExit(timeout);
                    var output = process.StandardOutput.ReadToEnd();
                    tcs.SetResult(output);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                    throw;
                }

            });

            return tcs.Task;
        }
    }
}
