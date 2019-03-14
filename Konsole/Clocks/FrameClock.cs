using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Konsole.Clocks
{
    public class FrameClock : IFrameClock
    {
        public FrameClock() => clock.Start();
        private Stopwatch clock = new Stopwatch();
        private uint previousTime;
        public uint ElapsedTime
        {
            get
            {
                previousTime = TimeMilliseconds;
                return TimeMilliseconds - previousTime;
            }
        }

        public uint TimeMilliseconds => (uint)clock.ElapsedMilliseconds;

        public double Time => (double)clock.ElapsedMilliseconds / 1000;
    }
}