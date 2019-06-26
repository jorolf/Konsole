using Konsole.Graphics.Colour;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Numerics;

namespace Konsole.Tests
{
    [TestFixture]
    public class ColourTests
    {
        static object[] canPointerCastFrom = new object[]
        {
            new Vector3(10, 12, -122),
            new Vector3(1f, 0.4243f, 12.1249f)
        };

        [Test, TestCaseSource(nameof(canPointerCastFrom))]
        public unsafe void CanPointerCastFromVector3(Vector3 vec)
        {
            Colour3 colour = *(Colour3*)&vec;

            Assert.AreEqual(colour.R, vec.X);
            Assert.AreEqual(colour.G, vec.Y);
            Assert.AreEqual(colour.B, vec.Z);
        }

        static object[] canPointerCastTo = new object[]
        {
            new Colour3(12, 29348f, -2932293),
            new Colour3(-10)
        };

        [Test, TestCaseSource(nameof(canPointerCastTo))]
        public unsafe void CanPointerCastToVector3(Colour3 c)
        {

            Vector3 vec = *(Vector3*)&c;

            Assert.AreEqual(c.R, vec.X);
            Assert.AreEqual(c.G, vec.Y);
            Assert.AreEqual(c.B, vec.Z);
        }

        [Test]
        public unsafe void HasSameSizeAsVec3()
        {
            int Vec3Size = sizeof(Vector3);
            int ColourSize = sizeof(Colour3);
            Assert.AreEqual(Vec3Size, ColourSize);
            Console.WriteLine($"Size of Vector3: {Vec3Size} Bytes");
            Console.WriteLine($"Size of Colour3: {ColourSize} Bytes");
        }
    }
}
