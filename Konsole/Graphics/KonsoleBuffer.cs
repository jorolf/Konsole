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
        public List<Drawable> Drawables = new List<Drawable>();
        
        
        public void GenerateGrid()
        {
            List<GridChar> grid = new List<GridChar>();
            for (int i = 0; i <= (Size.X * Size.Y); i++)
            {
                var vec = Vector2<int>.From1DArray(i, Size.Y);
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
                            if (gridChar.Position.X >= d.Position.X && gridChar.Position.X < d.Position.X + d.DrawSize.X)
                            {
                                if (gridChar.Position.Y >= d.Position.Y && gridChar.Position.Y < d.Position.Y + d.DrawSize.Y)
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
            KonsoleColour backgroundBuffer = KonsoleColour.Transparent;
            KonsoleColour foregroundBuffer = KonsoleColour.Transparent;
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
