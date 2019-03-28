using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Entities.Components
{
    public class DrawableComponent : Component
    {
        public override long ID => (long)IDs.Drawable;
    }
}
