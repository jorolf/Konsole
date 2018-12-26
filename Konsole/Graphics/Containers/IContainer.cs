using Konsole.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Containers
{
    public interface IContainer
    {
        IList<Drawable> Children { get; set; }
    }
}
