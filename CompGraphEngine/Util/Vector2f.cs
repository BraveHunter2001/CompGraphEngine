
namespace CompGraphEngine.Util
{
    public class Vector2f
    {
        public float X { get; set; }
        public float Y { get; set; }

        Vector2f(float x, float y)
        {
            X = x; Y = y; 
        }

       
        public static Vector2f operator +(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2f operator -(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.X - v2.X, v1.Y - v2.Y);
        }
    }
}
