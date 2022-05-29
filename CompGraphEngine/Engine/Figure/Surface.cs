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

    internal class Surface : GameObject
    {


       

        Color4 color = Color4.Red;

        public Surface()
        {
            Transform = new Transform();
        }
        public override void Init()
        {
            int row = 10, col = 10;
            
            var points = FillCoordsVertex(row, col);
            var colors = FillColorsVertex(row, col);

            renderObject = new RenderObjectsElements(points, colors,
                new Render.OpenGLAPI.Shader("Shaders/surface.glsl"),
                Transform.Model,
                GenerateIndices(row, col));

            renderObject.Init();

        }

        // this shit
        private int[] GenerateIndices(int row, int col)
        {
            

            List<int> indexes = new List<int>();
            
            int t = -1;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < col - 1; j++)
                {
                    indexes.Add(i * col + j);
                    indexes.Add(i * col + (j + 1) );
                    indexes.Add((i + 1) * col + j );

                    indexes.Add((i + 1) * col + j );
                    indexes.Add(i * col + (j + 1) );
                    indexes.Add((i + 1) * col + (j + 1));


                    //_indexes[++t] = i*col + j;
                    //_indexes[++t] = i*col + (j + 1);
                    //_indexes[++t] = (i+1)*col + j;

                    //_indexes[++t] = (i + 1)*col + j;
                    //_indexes[++t] = i * col + (j + 1);
                    //_indexes[++t] = (i+1) * col + (j + 1);
                }
            }

            return indexes.ToArray();

        }

        float[,] FillCoordsVertex(int countRowVert, int countColVert)
        {
            float[,] _vertPoints = new float[countRowVert * countColVert, 3];
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
            return _vertPoints;
        }
        float[,] FillColorsVertex(int countRowVert, int countColVert)
        {
            float[,] _vertColors = new float[countRowVert * countColVert, 4];
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
            return _vertColors;
        }

        public override void Update()
        {
            renderObject.Model = Transform.Model;
        }
    }
}
