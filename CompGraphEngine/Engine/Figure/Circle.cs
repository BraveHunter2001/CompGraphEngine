using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace CompGraphEngine.Engine.Figure
{
    public class Circle : Figure, IRenderable
    {
        int[] _indices = { 0, 1, 3, 1, 2, 3 };
        IndexBuffer _indexBuffer;
        public Matrix4 MVP;

        // this fucking local data
        // dont touch this shit
        Vector3 center = Vector3.Zero;
        float radius = 1f;
        public Color4 color = Color4.Blue;
        float thickness = 1;
        
        public float Thickness { get => thickness; set => thickness = value; }

        public Circle()
        {
            Transform = new Transform();
        }
        public Circle(Vector3 center)
        {
            Transform = new Transform();
            Transform.Position = center;
        }
        public Circle(Transform transform)
        {
            Transform = transform;
        }


        public override void Init()
        {
            FillCoordsVertex();
            FillColorsVertex();

            _indexBuffer = new IndexBuffer(_indices, _indices.Length);

            _shader = new Shader("Shaders/circle.glsl");


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


        public void Draw(Camera camera)
        {
           
        
            MVP = camera.GetProjection3D() * camera.GetViewMatrix() * Transform.Model;

            _shader.SetMatrix4("aMVP", MVP);
            _shader.SetFloat("aThickness",thickness);

            _shader.Use();
            _vertexArray.Bind();
            _indexBuffer.Bind();
            
            GL.DrawElements(PrimitiveType.Triangles,_indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
           
        }

        public bool isContain(float xOnWindow, float yOnWindow, Camera cam)
        {
            Vector3 point1 = new Vector3(center.X - radius, center.Y - radius, center.Z);
            //Vector3 point2 = new Vector3(center.X - radius, center.Y + radius, center.Z);
            Vector3 point3 = new Vector3(center.X + radius, center.Y + radius, center.Z);
           // Vector3 point4 = new Vector3(center.X + radius, center.Y - radius, center.Z);

            Vector2 pos1 = Helper.GetWindowPosObj(point1, cam);
            //Vector2 pos2 = Helper.GetWindowPosObj(point2, cam);
            Vector2 pos3 = Helper.GetWindowPosObj(point3, cam);
            //Vector2 pos4 = Helper.GetWindowPosObj(point4, cam);
           // Console.WriteLine($"Left bot: {pos1}, Right top:{pos3}");

            if (pos1.X <= xOnWindow && pos3.X >= xOnWindow && pos1.Y >= yOnWindow && pos3.Y <= yOnWindow)
                return true;
            return false;
        }
    }
}
