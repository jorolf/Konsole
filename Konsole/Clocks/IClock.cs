using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Clocks
{
    public interface IClock
    {
        /// <summary>
        /// This Clock's time in milliseconds.
        /// </summary>
        uint Time { get; }
    }
}
