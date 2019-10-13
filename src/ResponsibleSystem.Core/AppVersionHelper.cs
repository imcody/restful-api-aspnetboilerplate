using System;
using System.IO;
using Abp.Reflection.Extensions;

namespace ResponsibleSystem
{
    /// <summary>
    /// Central point for application version.
    /// </summary>
    public class AppVersionHelper
    {
        /// <summary>
        /// Gets current version of the application.
        /// It's also shown in the web page.
        /// </summary>
        public static AppVersion Version = new AppVersion(
            version: 1,
            releaseNumber: 1);

        /// <summary>
        /// Gets release (last build) date of the application.
        /// It's shown in the web page.
        /// </summary>
        public static DateTime ReleaseDate => new FileInfo(typeof(AppVersionHelper).GetAssembly().Location).LastWriteTime;
    }

    public class AppVersion
    {
        public int Version { get; set; }
        public int ReleaseNumber { get; set; }

        public AppVersion(int version, int releaseNumber)
        {
            Version = version;
            ReleaseNumber = releaseNumber;
        }

        public override string ToString()
        {
            return $"v{Version}.{ReleaseNumber:D2}";
        }
    }
}
