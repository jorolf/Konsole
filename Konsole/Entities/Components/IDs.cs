using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Entities.Components
{
    [Flags]
    public enum IDs : long
    {
        Positional = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001,
        Drawable =   0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000,
        Animatable = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000,
        Light =      0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000,

    }
}
