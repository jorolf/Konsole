using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Timing
{
    public interface IClock
    {
        double CurrentTime { get; }
        double Rate { get; }
        bool IsRunning { get; }
    }
}
