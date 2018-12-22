using System;
using System.Collections.Generic;
using System.Text;
using Konsole.Graphics.Colour;
using Konsole.Vectors;
using Konsole.Graphics.Enums;

namespace Konsole.Graphics.Drawables
{
    public abstract class Drawable : IDrawable
    {
        public Vector2 DrawSize { get; }
        public Vector2<int> Position { get; set; } = (Vector2<int>)0;
        public Vector2<int> Size { get; set; } = (Vector2<int>)0;
        /// <summary>
        /// The character that should be used to draw this <see cref="Drawable"/>
        /// </summary>  
        public char Fill = ' ';
        public KonsoleColour Colour { get; set; }
        public enum LoadState
        {
            Unloaded,
            Loading,
            Ready,
            Loaded
        }
        public Axes RelativeScale { get; set; }
        public Anchor Anchor { get; set; } = Anchor.TopLeft;
    }
}
