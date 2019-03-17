
using System.Drawing;

namespace Konsole.Extensions
{
    public static class ColorExtenions
    {
        public static Color Multiply(this Color colour, float num)
        {
            return Color.FromArgb((int)(colour.R * num), (int)(colour.G * num), (int)(colour.B * num));
        }
    }
}
