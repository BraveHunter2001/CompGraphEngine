using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Util
{
    public class Vector4f
    {
        public float x, y, z, t;

        public Vector4f(float x, float y, float z, float t)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.t = t;
        }
        public Vector4f(Color color)
        {
            this.x = color.R;
            this.y = color.G;
            this.z = color.B;
            this.t = color.A;
        }

        public static Vector4f operator +(Vector4f v1, Vector4f v2)
        {
            return new Vector4f(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.t + v2.t);
        }

        public static Vector4f operator -(Vector4f v1, Vector4f v2)
        {
            return new Vector4f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.t - v2.t);
        }

        public float[] ToArray()
        {
            return new float[] { x, y,z, t };
        }
    }
}
