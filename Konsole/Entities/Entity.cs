using Konsole.Entities.Components;
using System.Numerics;

namespace Konsole.Entities
{
    public abstract class Entity
    {
        public Component[] Components { get; private set; }
    }
}
