using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Gsof.Extensions;

namespace Gsof.Extensions
{
    public class ProcessEx
    {
        public static Task<string> Exec(string p_fileName, string p_args, int timeout = -1)
        {
            var process = new Process();

            process.StartInfo.FileName = p_fileName; ;
            process.StartInfo.Arguments = p_args;

            return process.Exec();
        }
    }
}
