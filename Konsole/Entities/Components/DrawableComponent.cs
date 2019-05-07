using Konsole.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Entities.Components
{
    public class DrawableComponent : Component
    {
        private PositionalComponent pos;
        public override long ID => (long)IDs.Drawable;
        public Drawable[] Drawables;

        public DrawableComponent(PositionalComponent p)
        {
            pos = p;
        }
    }
}
