using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Konsole.Primitives
{
    public struct Triangle
    {
        private Vector3 A;
        private Vector3 B;
        private Vector3 C;

        public Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// The normal of this <see cref="Triangle"/>
        /// </summary>
        public Vector3 Normal
        {
            get
            {
                return Vector3.Cross(B - A, C - A);
            }
        }
    }
}
