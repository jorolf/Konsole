using Konsole.Graphics.Colour;
using Konsole.Graphics.Containers;
using Konsole.Graphics.Drawables;
using Konsole.Graphics.Enums;
using Konsole.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class TestContainer : Container
    {
        public TestContainer()
        {
            Position = new Vector2<int>(10, 6);
            Size = new Vector2<int>(16, 12);
            RelativeSize = Axes.Both;
            Anchor = Anchor.Centre;
            Children = new Drawable[]
            {
              
                new Box()
                {
                    RelativeSize = Axes.Both,
                    Colour = KonsoleColour.LightGreen,
                    Fill = '█',
                },
                new Box()
                {
                    RelativeSize = Axes.X,
                    Anchor = Anchor.Centre,
                    Size = new Vector2<int>(2,2),
                    Fill = 'X',
                    Colour = KonsoleColour.LightBlue
                },
                new Box()
                {
                    RelativeSize = Axes.Y,
                    Anchor = Anchor.Centre,
                    Size = new Vector2<int>(4,2),
                    Fill = 'O',
                    Colour = KonsoleColour.Blue
                }
            };
        }
    }
}

