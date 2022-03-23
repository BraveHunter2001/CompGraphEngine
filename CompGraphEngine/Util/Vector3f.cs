
using CompGraphEngine.Engine;
using System.Drawing;

namespace CompGraphEngine.Util
{
    public class Vector3f
    {

        public float x, y, z;

        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3f(Color color)
        {
            this.x = color.R;
            this.y = color.G;
            this.z = color.B;
        }

        public static Vector3f operator +(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3f operator -(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public float[] ToArray()
        {
            this.NormalizeCoord();
            return new float[] {x,y, z};
        }

        public void NormalizeCoord()
        {
            x = (x - Constants.Width) / Constants.Width;
             y = (-1) * ((y- Constants.Height)/Constants.Height);
        }
    }
}
