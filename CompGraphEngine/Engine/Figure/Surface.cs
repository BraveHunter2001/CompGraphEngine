using CompGraphEngine.Render;
using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace CompGraphEngine.Engine.Figure
{

    internal class Surface : Figure
    {


        int[] _indexes;
        IndexBuffer _indexBuffer;
        Matrix4 MVP;

        Color4 color = Color4.Red;

        public Surface()
        {
            Transform = new Transform();
        }
        public override void Init()
        {
            int row = 10, col = 10;
            FillCoordsVertex(row, col);
            FillColorsVertex(row, col);

            GenerateIndices(row, col);


            _indexBuffer = new IndexBuffer(_indexes, _indexes.Length);
            _shader = new Shader("Shaders/surface.glsl");

            _indexes = null;
            base.Init();

        }

        // this shit
        private void GenerateIndices(int row, int col)
        {


            List<int> indexes = new List<int>();

            int t = -1;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < col - 1; j++)
                {
                    indexes.Add(i * col + j);
                    indexes.Add(i * col + (j + 1));
                    indexes.Add((i + 1) * col + j);

                    indexes.Add((i + 1) * col + j);
                    indexes.Add(i * col + (j + 1));
                    indexes.Add((i + 1) * col + (j + 1));


                    //_indexes[++t] = i*col + j;
                    //_indexes[++t] = i*col + (j + 1);
                    //_indexes[++t] = (i+1)*col + j;

                    //_indexes[++t] = (i + 1)*col + j;
                    //_indexes[++t] = i * col + (j + 1);
                    //_indexes[++t] = (i+1) * col + (j + 1);
                }
            }

            _indexes = indexes.ToArray();

        }

        void FillCoordsVertex(int countRowVert, int countColVert)
        {
            _vertPoints = new float[countRowVert * countColVert, 3];
            int t = 0;
            int si = 0;
            for (int i = 0; i < countRowVert; i++)
            {
                for (int j = 0; j < countColVert; j++)
                {
                    _vertPoints[t, 0] = i * 1.0f;
                    _vertPoints[t, 1] = (float)MathHelper.Sin(Math.PI / 8 * i) * (float)MathHelper.Sin(Math.PI / 8 * j) * 5f;
                    _vertPoints[t, 2] = j * 1.0f;
                    t++;
                }
                si++;
            }

        }
        void FillColorsVertex(int countRowVert, int countColVert)
        {
            _vertColors = new float[countRowVert * countColVert, 4];
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

        public void updateTime(float time)
        {
            _shader.SetFloat("vTime", time);
        }

        public void Draw(Camera camera)
        {
            MVP = camera.GetProjection3D() * camera.GetViewMatrix() * Transform.Model;

            _shader.SetMatrix4("aMVP", MVP);
            _shader.Use();

            _vertexArray.Bind();
            _indexBuffer.Bind();

            GL.DrawElements(PrimitiveType.Triangles, _indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
        }
    }
}
