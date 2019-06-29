using System;
using osu.Framework;
using osu.Framework.Konsole;
using osu.Framework.Platform;

namespace SampleGame.Konsole
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            using GameHost host = Host.GetSuitableHost(@"sample-game");
            using Game game = new SampleGameGame();

            host.HookKonsole();
            host.Run(game);
        }
    }
}
