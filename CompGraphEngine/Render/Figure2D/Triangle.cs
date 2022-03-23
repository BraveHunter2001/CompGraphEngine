
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
            _countVertex = 3;
            float[][] posVerts = new float[_countVertex][];
            posVerts[0] = point1.ToArray();
            posVerts[1] = point2.ToArray();
            posVerts[2] = point3.ToArray();
            _vertices = toArrayFromPoints(posVerts, new Vector4f(Color), _sizePosition, _sizeColor);
            
        }
        public Triangle(Vector3f point1, Vector3f point2, Vector3f point3, Color Color)
        {

            this.Color = Color;
            _countVertex = 3;
            float[][] posVerts = new float[_countVertex][];
            posVerts[0] = point1.ToArray();
            posVerts[1] = point2.ToArray();
            posVerts[2] = point3.ToArray();
            _vertices = toArrayFromPoints( posVerts, new Vector4f(Color), _sizePosition, _sizeColor);
           

        }
        public Triangle(Vector3f point1, Vector3f point2, Vector3f point3, Vector4f color)
        {
            _countVertex = 3;
            float[][] posVerts = new float[_countVertex][];
            posVerts[0] = point1.ToArray();
            posVerts[1] = point2.ToArray();
            posVerts[2] = point3.ToArray();

            _vertices = toArrayFromPoints(posVerts, color, _sizePosition, _sizeColor);
            

        }

       

        public Triangle(float[] vert)
        {
            _vertices = vert;
            
        }
        public override void Init()
        {
            base.Init();
            _shader = new Shader("Shaders/triangle.glsl");

            _layout.Push<float>(_sizePosition, false);
            _layout.Push<float>(_sizeColor, false);
            _vertexArray.AddBuffer(ref _vertexBuffer, ref _layout);
        }
        public override void Draw()
        {
            _shader.Use();
            _vertexArray.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertexBuffer.CountVertex);
        }
    }
}
