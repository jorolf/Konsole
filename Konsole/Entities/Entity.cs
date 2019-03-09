using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Konsole.Entities
{
    public abstract class Entity
    {
        public abstract Vector3 Position { get; }
        public abstract Vector3 Rotation { get; }
        public abstract Vector3 Scale { get; }
    }
}
