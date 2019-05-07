using System.Numerics;

namespace Konsole.Graphics.Drawables
{
    public class Drawable : IDrawable
    {
        public Vector3 Scale { get; set; } = Vector3.One;
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero;
        public Vector3 Origin { get; set; } = Vector3.Zero;

        public Matrix4x4 DrawableMatrix => Matrix4x4.CreateTranslation(Origin) * Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z) * Matrix4x4.CreateTranslation(Position);

        public Mesh[] Meshes { get; set; } = new Mesh[] { new Mesh() };
    }
}
