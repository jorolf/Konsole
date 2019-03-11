using System.Numerics;

namespace Konsole.Graphics.Drawables
{
    public interface IDrawable
    {
        Vector3 Scale { get; }
        Vector3 Position { get; }
        Vector3 Rotation { get; }
        Matrix4x4 ModelMatrix { get; }
        Mesh Mesh { get; }
    }
}

