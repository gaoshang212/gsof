using System;
using System.IO;
using System.Linq;

namespace Gsof.Extensions
{
    public static class PathExtension
    {
        public static string GetAppData(params string[] p_paths)
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Combine(appdata, p_paths);
        }

        public static string GetLocalAppData(params string[] p_paths)
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Combine(appdata, p_paths);
        }

        public static string Combine(string p_path, params string[] p_paths)
        {
            var paths = new[] { p_path }.Concat(p_paths).ToArray();
            var dst = Path.Combine(paths);

            return dst;
        }

        /// <summary>
        /// Get Home Dir 
        /// </summary>
        /// <param name="p_paths"></param>
        /// <returns></returns>
        public static string GetHome(params string[] p_paths)
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                               Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            return Combine(homePath, p_paths);
        }

    }
}
