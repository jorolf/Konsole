using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Colour
{
    public enum KonsoleColour
    {
        Transparent,
        Black,
        //DarkGrey,
        //LightGrey,
        White,
        Red,
        //LightRed,
        Green,
        //LightGreen,
        Blue,
        //LightBlue,
        Cyan,
        //LightCyan,
        Magenta,
        //LightMagenta,
        Yellow,
        //LightYellow
    }
    
    public static class KonsoleColourMethods
    {
        public static string ToBackgroundColour(this KonsoleColour colour)
        {
            switch (colour)
            {
                default:
                    return "\u001b[40m";
                //case KonsoleColour.DarkGrey:
                //    return "\u001b[40;1m";
                case KonsoleColour.White:
                    return "\u001b[47m";
                //case KonsoleColour.White:
                //    return "\u001b[47;1m";
                case KonsoleColour.Red:
                    return "\u001b[41m";
                //case KonsoleColour.LightRed:
                //    return "\u001b[41;1m";
                case KonsoleColour.Green:
                    return "\u001b[42m";
                //case KonsoleColour.LightGreen:
                //    return "\u001b[42;1m";
                case KonsoleColour.Blue:
                    return "\u001b[44m";
                //case KonsoleColour.LightBlue:
                //    return "\u001b[44;1m";
                case KonsoleColour.Cyan:
                    return "\u001b[46m";
                //case KonsoleColour.LightCyan:
                //    return "\u001b[46;1m";
                case KonsoleColour.Magenta:
                    return "\u001b[45m";
                //case KonsoleColour.LightMagenta:
                //   return "\u001b[45;1m";
                case KonsoleColour.Yellow:
                    return "\u001b[43m";
                //case KonsoleColour.LightYellow:
                //   return "\u001b[43;1m";
            }
        }
        public static string ToForegroundColour(this KonsoleColour colour)
        {
            switch (colour)
            {
                default:
                    return "\u001b[30m";
                //case KonsoleColour.DarkGrey:
                //    return "\u001b[30;1m";
                case KonsoleColour.White:
                    return "\u001b[37m";
                //case KonsoleColour.White:
                //    return "\u001b[37;1m";
                case KonsoleColour.Red:
                    return "\u001b[31m";
                //case KonsoleColour.LightRed:
                //    return "\u001b[31;1m";
                case KonsoleColour.Green:
                    return "\u001b[32m";
                //case KonsoleColour.LightGreen:
                //    return "\u001b[32;1m";
                case KonsoleColour.Blue:
                    return "\u001b[34m";
                //case KonsoleColour.LightBlue:
                //    return "\u001b[34;1m";
                case KonsoleColour.Cyan:
                    return "\u001b[36m";
                //case KonsoleColour.LightCyan:
                //   return "\u001b[36;1m";
                case KonsoleColour.Magenta:
                    return "\u001b[35m";
                //case KonsoleColour.LightMagenta:
                //    return "\u001b[35;1m";
                case KonsoleColour.Yellow:
                    return "\u001b[33m";
                //case KonsoleColour.LightYellow:
                //    return "\u001b[33;1m";
            }
        }
    }
}
