using CompGraphEngine.Util;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render.Figure2D
{
    internal class Line : Figure
    {
        const int _sizePosition = 3, _sizeColor = 4;
        
        private protected Color Color { get; set; } = Color.White;

        public Line(Vector3f point1, Vector3f point2, Color color)
        {
            _countVertex = 2;
            float[][] posVerts = new float[_countVertex][];
            posVerts[0] = point1.ToArray();
            posVerts[1] = point2.ToArray();
            this.Color = color;
            _vertices = toArrayFromPoints( posVerts, new Vector4f(Color), _sizePosition, _sizeColor);
            Init();
        }
        public Line (Vector3f point1, Vector3f point2)
        {
            _countVertex = 2;
            float[][] posVerts = new float[_countVertex][];
            posVerts[0] = point1.ToArray();
            posVerts[1] = point2.ToArray();

            _vertices = toArrayFromPoints(posVerts, new Vector4f(Color), _sizePosition, _sizeColor);

            Init();
        }
        public Line(float[] vert) 
        {
            _vertices = vert;
            Init();
        }
        public override void Init()
        {
            base.Init();
            _shader = new Shader("Shaders/line.glsl");

            _layout.Push<float>(_sizePosition, false);
            _layout.Push<float>(_sizeColor, false);
            _vertexArray.AddBuffer(ref _vertexBuffer, ref _layout);
        }

        public void TestCoordsRedraws(float x, float y)
        {
            ///posVerts[1] = new float[3] {x, y, 0};
        }
        public override void Draw()
        {
           


            _shader.Use();
            _vertexArray.Bind();
            GL.Enable(EnableCap.LineSmooth);
            GL.LineWidth(1.0f);
            
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertexBuffer.CountVertex);
            GL.Disable(EnableCap.LineSmooth);
        }

    }
}
