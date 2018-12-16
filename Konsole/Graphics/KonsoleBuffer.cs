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
                Position = (Vector2<int>)10,
                Size = (Vector2<int>)5,
                Fill = '█'
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
                buffer += gridItem.Char;
            }
            Console.Clear();
            Console.Write(buffer);
        }
    }
}
