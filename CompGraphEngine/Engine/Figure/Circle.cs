using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace CompGraphEngine.Engine.Figure
{
    public class Circle : Figure2D, IRenderable
    {
        int[] _indices = { 0, 1, 3, 1, 2, 3 };
        IndexBuffer _indexBuffer;


        Vector3 center;
        float radius;
        Color4 color = new Color4(255, 255, 255, 255);
        public Vector3 Center{ get => center; set => center = value; }
        public float Radius { get => radius; set => radius = value; }

        public Circle(Vector3 center, float radius)
        {
            this.radius = radius;
            this.center = center;
        }
        public Circle(Vector3 center, float radius, Color4 color)
        {
            this.radius = radius;
            this.center = center;
            this.color = color;
        }

        public override void Init()
        {
            FillCoordsVertex();
            FillColorsVertex();
            _indexBuffer = new IndexBuffer(_indices, _indices.Length);

           

            _shader = new Shader("Shaders/circle.glsl");
           //_shader.SetVector2("uResolution", Window.window.Size);
            base.Init();
        }

        void FillCoordsVertex()
        {
            _vertPoints = new float[4, 3];

            //vertex - 0
            _vertPoints[0, 0] = center.X - radius;
            _vertPoints[0, 1] = center.Y - radius;
            _vertPoints[0, 2] = center.Z;

            //vertex - 1
            _vertPoints[1, 0] = center.X - radius;
            _vertPoints[1, 1] = center.Y + radius;
            _vertPoints[1, 2] = center.Z;

            //vertex - 2
            _vertPoints[2, 0] = center.X + radius;
            _vertPoints[2, 1] = center.Y + radius;
            _vertPoints[2, 2] = center.Z;

            //vertex - 3
            _vertPoints[3, 0] = center.X + radius;
            _vertPoints[3, 1] = center.Y - radius;
            _vertPoints[3, 2] = center.Z;

        }
        void FillColorsVertex()
        {
            //color
            _vertColors = new float[4, 4];
            for (int i = 0; i < 4; i++)
            {
                _vertColors[0, i] = ((Vector4)color)[i];
                _vertColors[1, i] = ((Vector4)color)[i];
                _vertColors[2, i] = ((Vector4)color)[i];
                _vertColors[3, i] = ((Vector4)color)[i];
            }
        }

        public void Draw()
        {
            _shader.Use();
            _vertexArray.Bind();
            _indexBuffer.Bind();
            
            GL.DrawElements(PrimitiveType.Triangles,_indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
           
        }
    }
}
