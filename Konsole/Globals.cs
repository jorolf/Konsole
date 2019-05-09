using System.IO;
using System.Reflection;

namespace Konsole
{
    public static class Globals
    {
        public static string GameDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;
    }
}
