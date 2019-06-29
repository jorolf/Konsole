﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using osu.Desktop;
using osu.Framework;
using osu.Framework.Development;
using osu.Framework.Konsole;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osu.Game.IPC;
using osu.Game.Tournament;

namespace osu.Konsole
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            // Back up the cwd before DesktopGameHost changes it
            var cwd = Environment.CurrentDirectory;

            using (DesktopGameHost host = Host.GetSuitableHost(@"osu", true))
            {
                host.ExceptionThrown += handleException;

                if (!host.IsPrimaryInstance)
                {
                    var importer = new ArchiveImportIPCChannel(host);
                    // Restore the cwd so relative paths given at the command line work correctly
                    Directory.SetCurrentDirectory(cwd);

                    foreach (var file in args)
                    {
                        Console.WriteLine(@"Importing {0}", file);
                        if (!importer.ImportAsync(Path.GetFullPath(file)).Wait(3000))
                            throw new TimeoutException(@"IPC took too long to send");
                    }
                }
                else
                {
                    host.HookKonsole();
                    switch (args.FirstOrDefault() ?? string.Empty)
                    {
                        default:
                            host.Run(new OsuGameDesktop(args));
                            break;

                        case "--tournament":
                            host.Run(new TournamentGame());
                            break;
                    }
                }

                return 0;
            }
        }

        private static int allowableExceptions = DebugUtils.IsDebugBuild ? 0 : 1;

        /// <summary>
        /// Allow a maximum of one unhandled exception, per second of execution.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static bool handleException(Exception arg)
        {
            bool continueExecution = Interlocked.Decrement(ref allowableExceptions) >= 0;

            Logger.Log($"Unhandled exception has been {(continueExecution ? $"allowed with {allowableExceptions} more allowable exceptions" : "denied")} .");

            // restore the stock of allowable exceptions after a short delay.
            Task.Delay(1000).ContinueWith(_ => Interlocked.Increment(ref allowableExceptions));

            return continueExecution;
        }
    }
}
