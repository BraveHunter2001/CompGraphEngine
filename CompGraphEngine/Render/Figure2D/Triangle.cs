using CompGraphEngine.Scene;
using CompGraphEngine.Util;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace CompGraphEngine.Render.Figure2D
{
    public class Triangle : Figure
    {
        const int _sizePosition = 3, _sizeColor = 4;
        //public Transform Transform { get; set; }
        private protected Color Color { get; set; }  = Color.White;

        public Triangle(Vector3f point1, Vector3f point2, Vector3f point3)
        {
            float[] vert = new float[(_sizePosition + _sizeColor) * 3];
            Vector4f color = new Vector4f(Color);
            int shift = 0;

            PointToArr(ref vert, point1, ref shift);
            shift += _sizeColor;
            PointToArr(ref vert, point2, ref shift);
            shift += _sizeColor;
            PointToArr(ref vert, point3, ref shift);

            for (int i = 0; i < 3; i++)
            {
                
                int s = (_sizePosition + _sizeColor) * i;

                for (int j = 0;j <4;j++)
                {
                    int curr = (_sizeColor - 1 + j);
                    vert[curr + s] = color.ToArray()[j];
                }
                    
            }
            _vertices = vert;
            Init();

        }

        public Triangle(Vector3f point1, Vector3f point2, Vector3f point3, Color Color)
        {
            float[] vert = new float[(_sizePosition + _sizeColor) * 3];
            int shift = 0;

            this.Color = Color;

            Vector4f color = new Vector4f(Color);
            PointToArr(ref vert, point1, ref shift);
            shift += _sizeColor;
            PointToArr(ref vert, point2, ref shift);
            shift += _sizeColor;
            PointToArr(ref vert, point3, ref shift);

            for (int i = 0; i < 3; i++)
            {

                int s = (_sizePosition + _sizeColor) * i;

                for (int j = 0; j < 4; j++)
                {
                    int curr = (_sizeColor - 1 + j);
                    vert[curr + s] = color.ToArray()[j];
                }

            }
            _vertices = vert;
            Init();

        }
        public Triangle(Vector3f point1, Vector3f point2, Vector3f point3, Vector4f color)
        {
            float[] vert = new float[(_sizePosition + _sizeColor) * 3];
            int shift = 0;

            PointToArr(ref vert, point1, ref shift);
            shift += _sizeColor;
            PointToArr(ref vert, point2, ref shift);
            shift += _sizeColor;
            PointToArr(ref vert, point3, ref shift);

            for (int i = 0; i < 3; i++)
            {

                int s = (_sizePosition + _sizeColor) * i;

                for (int j = 0; j < 4; j++)
                {
                    int curr = (_sizeColor - 1 + j);
                    vert[curr + s] = color.ToArray()[j];
                }

            }
            _vertices = vert;
            Init();

        }

        private void PointToArr(ref float[] arr, Vector3f p,ref int shift)
        {
            for (int i = 0; i < p.ToArray().Length; i++)
            {
                arr[shift] = p.ToArray()[i];
                shift++;
            }
        }

        public Triangle(float[] vert)
        {
            _vertices = vert;
            Init();
        }
        public override void Init()
        {
            base.Init();
            _shader = new Shader("Shaders/triangle.glsl");

            _layout.Push<float>(_sizePosition, false);
            _layout.Push<float>(_sizeColor, false);
            _vertexArray.AddBuffer(ref _vertexBuffer, ref _layout);
        }
        internal override void Draw()
        {
            _shader.Use();
            _vertexArray.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertexBuffer.CountVertex);
        }
    }
}
