using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Colour
{
    public enum KonsoleColour
    {
        Transparent,
        Black,
        DarkGrey,
        LightGrey,
        White,
        Red,
        LightRed,
        Green,
        LightGreen,
        Blue,
        LightBlue,
        Cyan,
        LightCyan,
        Magenta,
        LightMagenta,
        Yellow,
        LightYellow
    }
    
    public static class KonsoleColourMethods
    {
        public static string ToBackgroundColour(this KonsoleColour colour)
        {
            switch (colour)
            {
                default:
                    return "\u001b[40m";
                case KonsoleColour.DarkGrey:
                    return "\u001b[40;1m";
                case KonsoleColour.LightGrey:
                    return "\u001b[47m";
                case KonsoleColour.White:
                    return "\u001b[107m";
                case KonsoleColour.Red:
                    return "\u001b[41m";
                case KonsoleColour.LightRed:
                    return "\u001b[101m";
                case KonsoleColour.Green:
                    return "\u001b[42m";
                case KonsoleColour.LightGreen:
                    return "\u001b[102m";
                case KonsoleColour.Blue:
                    return "\u001b[44m";
                case KonsoleColour.LightBlue:
                    return "\u001b[104m";
                case KonsoleColour.Cyan:
                    return "\u001b[46m";
                case KonsoleColour.LightCyan:
                    return "\u001b[106m";
                case KonsoleColour.Magenta:
                    return "\u001b[45m";
                case KonsoleColour.LightMagenta:
                   return "\u001b[105m";
                case KonsoleColour.Yellow:
                    return "\u001b[43m";
                case KonsoleColour.LightYellow:
                   return "\u001b[103m";
            }
        }
        public static string ToForegroundColour(this KonsoleColour colour)
        {
            switch (colour)
            {
                default:
                    return "\u001b[30m";
                case KonsoleColour.DarkGrey:
                    return "\u001b[30;1m";
                case KonsoleColour.LightGrey:
                    return "\u001b[37m";
                case KonsoleColour.White:
                    return "\u001b[97m";
                case KonsoleColour.Red:
                    return "\u001b[31m";
                case KonsoleColour.LightRed:
                    return "\u001b[91m";
                case KonsoleColour.Green:
                    return "\u001b[32m";
                case KonsoleColour.LightGreen:
                    return "\u001b[92m";
                case KonsoleColour.Blue:
                    return "\u001b[34m";
                case KonsoleColour.LightBlue:
                    return "\u001b[94m";
                case KonsoleColour.Cyan:
                    return "\u001b[36m";
                case KonsoleColour.LightCyan:
                    return "\u001b[96m";
                case KonsoleColour.Magenta:
                    return "\u001b[35m";
                case KonsoleColour.LightMagenta:
                    return "\u001b[95m";
                case KonsoleColour.Yellow:
                    return "\u001b[33m";
                case KonsoleColour.LightYellow:
                    return "\u001b[93m";
            }
        }
    }
}
