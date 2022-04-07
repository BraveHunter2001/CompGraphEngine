using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine.Figure
{
    internal class Surface : Figure, IRenderable
    {


        int[] _indices;
        IndexBuffer _indexBuffer;
        Matrix4 MVP;
        Color4 color = Color4.Red;

        public Surface()
        {
            Transform = new Transform();
        }
        public override void Init()
        {
            int quadrVector = 3;
            FillCoordsVertex(quadrVector, quadrVector);
            FillColorsVertex(quadrVector, quadrVector);
            _indices = new int[(quadrVector * quadrVector) * 6];
            for (int i = 0; i < (quadrVector - 1) * (quadrVector - 1); i++)
            {
                GenerateIndices(i);
            }
             // rect = (row - 1) * (col -1)
        


            _indexBuffer = new IndexBuffer(_indices, _indices.Length);
            _shader = new Shader("Shaders/surface.glsl");
            base.Init();
        }

        private void GenerateIndices( int index)
        {
            int offsetArrayIndex = 6 * index;
            int offset = 4 * index;

            // 3, 2, 0, 0, 2, 1        7, 6, 4, 4, 6, 5
            // Triangle 1
            _indices[offsetArrayIndex] = offset + 3;
            _indices[offsetArrayIndex + 1] = offset + 2;
            _indices[offsetArrayIndex + 2] = offset + 0;

            // Triangle 2
            _indices[offsetArrayIndex + 3] = offset + 0;
            _indices[offsetArrayIndex + 4] = offset + 2;
            _indices[offsetArrayIndex + 5] = offset + 1;
        }

        void FillCoordsVertex(int countRowVert,int  countColVert)
        {
            _vertPoints = new float[countRowVert * countColVert, 3];
            float offset = 0.0f;
            int count = 0;
            for (int i = 0; i < countRowVert; i++)
            {
                for (int j = 0; j < countColVert; j++)
                {
                    _vertPoints[count, 0] = new Random().Next(-100, 100) * 0.01f;
                    _vertPoints[count, 1] = new Random().Next(-100, 100) * 0.01f;
                    _vertPoints[count, 2] = new Random().Next(-100, 100) * 0.01f;
                    offset += 0.1f * (countColVert + 1);
                    count++;
                }
                offset += 0.1f * (countRowVert + 1);
            }

        }
        void FillColorsVertex(int countRowVert, int countColVert)
        {
            _vertColors= new float[countRowVert * countColVert, 4];
            int count = 0;

            for (int i = 0; i < countRowVert; i++)
            {
                for (int j = 0; j < countColVert; j++)
                {
                    _vertColors[count, 0] = color.R;
                    _vertColors[count, 1] = color.G;
                    _vertColors[count, 2] = color.B;
                    _vertColors[count, 3] = color.A;
                    count++;
                }
                
            }

        }
        public void Draw(Camera camera)
        {
            MVP = camera.GetProjection() * camera.GetViewMatrix() * Transform.Model;

            _shader.SetMatrix4("aMVP", MVP);
            _shader.Use();
            _vertexArray.Bind();
            _indexBuffer.Bind();

            GL.DrawElements(PrimitiveType.Triangles, _indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
        }
    }
}
