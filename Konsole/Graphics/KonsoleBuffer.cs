using Konsole.Graphics.Colour;
using Konsole.Graphics.Containers;
using Konsole.Graphics.Drawables;
using Konsole.Graphics.Enums;
using Konsole.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics
{
    public class KonsoleBuffer
    {
        private StringBuilder buffer = new StringBuilder();

        public Vector2<int> Size;
        public GridChar[] Grid;
        public List<Drawable> Drawables = new List<Drawable>()
        {
            new Container()
            {
                Position = new Vector2<int>(10, 2),
                Size = (Vector2<int>)12,
                //Colour = KonsoleColour.Green,
                //Fill = '█',
                Children = new List<Drawable>()
                {
                    new Box()
                    {
                        Size = (Vector2<int>)12,
                        Colour = KonsoleColour.Green,
                        Fill = '█',
                    },
                    new Box()
                    {
                        Anchor = Anchor.Centre,
                        Size = (Vector2<int>)4,
                        Fill = '█',
                        Colour = KonsoleColour.Blue
                    }
                }

            }
        };
        private List<Drawable> draw = new List<Drawable>();
        
        public KonsoleBuffer(Vector2<int> size)
        {
            Size = size;
            List<GridChar> grid = new List<GridChar>();
            for (int i = 0; i <= (Size.X * Size.Y); i++)
            {
                var vec = Vector2<int>.ToVector2(i, Size);
                grid.Add(new GridChar(vec));
            }
            Grid = grid.ToArray();
        }
        
        public void RenderBuffer()
        {
            foreach (Drawable drawable in Drawables)
            {
                if (drawable is Container)
                {
                    foreach (Drawable d in (drawable as Container).GetChildren())
                    {
                        foreach (GridChar gridChar in Grid)
                        {
                            if (gridChar.Position.X >= d.Position.X && gridChar.Position.X < d.Position.X + d.Size.X)
                            {
                                if (gridChar.Position.Y >= d.Position.Y && gridChar.Position.Y < d.Position.Y + d.Size.Y)
                                {
                                    gridChar.Char = d.Fill;
                                    gridChar.BackgroundColour = gridChar.ForegroundColour;
                                    gridChar.ForegroundColour = d.Colour;
                                }
                            }
                        }
                    }
                }                
            }
        }

        public void PushToConsole()
        {
            KonsoleColour backgroundBuffer = KonsoleColour.Black;
            KonsoleColour foregroundBuffer = KonsoleColour.Black;
            foreach (GridChar gridItem in Grid)
            {
                if (backgroundBuffer == gridItem.BackgroundColour && foregroundBuffer == gridItem.ForegroundColour)               
                    buffer.Append(gridItem.Char);
                else
                {
                    buffer.Append("\u001b[0m");
                    buffer.Append(gridItem.BackgroundColour.ToBackgroundColour());
                    buffer.Append(gridItem.ForegroundColour.ToForegroundColour());
                    buffer.Append(gridItem.Char);
                    backgroundBuffer = gridItem.BackgroundColour;
                    foregroundBuffer = gridItem.ForegroundColour;
                }
            }
            Console.Write(buffer);
            buffer.Clear();
        }
    }
}
