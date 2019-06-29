using System;
using System.Linq;
using osu.Framework;
using osu.Framework.Konsole;
using osu.Framework.Platform;
using osu.Framework.Tests;

namespace SampleGame.Konsole
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            bool benchmark = args.Contains(@"--benchmark");
            bool portable = args.Contains(@"--portable");

            using (GameHost host = Host.GetSuitableHost(@"visual-tests", portableInstallation: portable))
            {
                host.HookKonsole();
                if (benchmark)
                    host.Run(new AutomatedVisualTestGame());
                else
                    host.Run(new VisualTestGame());
            }
        }
    }
}
