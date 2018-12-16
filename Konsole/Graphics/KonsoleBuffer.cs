using Konsole.Graphics.Drawables;
using Konsole.Vectors;
using System;
using System.Collections.Generic;

namespace Konsole.Graphics
{
    public class KonsoleBuffer
    {
        public Vector2<int> Size;
        public GridChar[] Grid;
        public Box[] Drawables = new Box[]
        {
            new Box()
            {
                Position = (Vector2<int>)6,
                Size = (Vector2<int>)15,
                Fill = '█',
                FillColour = ConsoleColor.Blue
            },
            new Box()
            {
                Position = (Vector2<int>)5,
                Size = (Vector2<int>)10,
                Fill = 'X',
                FillColour = ConsoleColor.Red
            },
            new Box()
            {
                Position = (Vector2<int>)4,
                Size = (Vector2<int>)6,
                Fill = 'O',
                FillColour = ConsoleColor.White
            }

        };

        
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
            foreach (Box drawable in Drawables)
            {
                foreach (GridChar gridChar in Grid)
                {
                    if (gridChar.Position.X >= drawable.Position.X && gridChar.Position.X < drawable.Position.X + drawable.Size.X)
                    {
                        if (gridChar.Position.Y >= drawable.Position.Y && gridChar.Position.Y < drawable.Position.Y + drawable.Size.Y)
                        {
                            gridChar.Char = drawable.Fill;
                            gridChar.BackgroundColour = gridChar.ForegroundColour;
                            gridChar.ForegroundColour = drawable.FillColour;
                        }
                    }
                }
            }
        }

        public void PushToConsole()
        {
            string buffer = "";
            foreach (GridChar gridItem in Grid)
            {
                if (Console.BackgroundColor == gridItem.BackgroundColour && Console.ForegroundColor == gridItem.ForegroundColour)
                {
                    buffer += gridItem.Char;
                }
                else
                {
                    Console.Write(buffer);
                    Console.BackgroundColor = gridItem.BackgroundColour;
                    Console.ForegroundColor = gridItem.ForegroundColour;
                    buffer = gridItem.Char.ToString();
                }
            }
            //Console.Clear();

        }
    }
}
