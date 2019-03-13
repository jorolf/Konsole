using System.Numerics;

namespace Konsole.Entities
{
    public abstract class Entity
    {
        public abstract Vector3 Position { get; }
        public abstract Vector3 Rotation { get; }
        public abstract Vector3 Scale { get; }
    }
}
