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
    internal class BSurface: Figure, IRenderable
    {
        int[] _indexes;
        IndexBuffer _indexBuffer;
        Matrix4 MVP;
        Color4 color = Color4.Red;

        List<int> KnotsT;
        List<int> KnotsU;

        public List<List<Circle>> ControlPoints;


        public List<List<List<List<float>>>> coefs;





        int degree = 3, offset = 10, controlSizeT = 10, controlSizeU = 10;
        public BSurface()
        {
            Transform = new Transform();

            ControlPoints = GenerateRandomControlPoints();

            KnotsT = GenerateKnots(degree, controlSizeT);
            KnotsU = GenerateKnots(degree, controlSizeU);

            coefs = GenerateCoef(controlSizeT, controlSizeU, offset, degree, KnotsT, KnotsU);

            FillCoordsVertex();
            FillColorsVertex();
            GenerateIndices(KnotsT[degree + controlSizeT] * offset, KnotsU[degree + controlSizeU] * offset);
        }

        public override void Init()
        {
            _indexBuffer = new IndexBuffer(_indexes, _indexes.Length);
            _shader = new Shader("Shaders/surface.glsl");

            _indexes = null;
            base.Init();
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
        private void GenerateIndices(int row, int col)
        {


            List<int> indexes = new List<int>();

            int t = -1;
            for (int i = 0; i < row-2; i++)
            {
                for (int j = 0; j < col-2; j++)
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

        void FillCoordsVertex()
        {
            int size = KnotsT[degree + controlSizeT] * offset
                * KnotsU[degree + controlSizeU] * offset;
            _vertPoints = new float[size, 3];
            int shift = 0;
           
            for (int t = 0; t < KnotsT[degree + controlSizeT] * offset ; t++)
            {
                for (int u = 0; u < KnotsU[degree + controlSizeU] * offset; u++)
                {
                    for (int controlT = 0; controlT < controlSizeT; controlT++)
                    {
                        for (int controlU = 0; controlU < controlSizeU; controlU++)
                        {
                            _vertPoints[shift, 0] += ControlPoints[controlT][controlU].Transform.Position.X
                                * coefs[t][u][controlT][controlU];
                            _vertPoints[shift, 1] += ControlPoints[controlT][controlU].Transform.Position.Y 
                                * coefs[t][u][controlT][controlU];
                            _vertPoints[shift, 2] += ControlPoints[controlT][controlU].Transform.Position.Z 
                                * coefs[t][u][controlT][controlU];
                            
                        }
                    }
                    shift++;
                }
               
            }

        }
        void FillColorsVertex()
        {
            int size = KnotsT[degree + controlSizeT] * offset
                * KnotsU[degree + controlSizeU] * offset;
            _vertColors = new float[size, 4];
            int count = 0;

            for (int i = 0; i < KnotsT[degree + controlSizeT] * offset; i++)
            {
                for (int j = 0; j < KnotsU[degree + controlSizeU] * offset; j++)
                {
                    _vertColors[count, 0] = color.R;
                    _vertColors[count, 1] = color.G;
                    _vertColors[count, 2] = color.B;
                    _vertColors[count, 3] = color.A;
                    count++;
                }

            }

        }


        private float GenerateN(int degree, int control, List<int> knots, float t)
        {
            if (degree == 0)
            {
                if ((knots[control] <= t) && (t <= knots[control + 1])) return 1.0f;
                return 0.0f;
            }

            float memb1 = 0.0f, memb2 = 0.0f;
            if (knots[degree + control] == knots[control])
                memb1 = 0.0f;
            else
            {
                memb1 = ((t - knots[control]) / (knots[degree + control] - knots[control])) * GenerateN(degree - 1, control, knots, t);
            }

            if (knots[degree + control + 1] == knots[control + 1])
                memb2 = 0.0f;
            else
                memb2 = ((knots[degree + control + 1] - t) / (knots[degree + control + 1] - knots[control + 1])) * GenerateN(degree - 1, control + 1, knots, t);
            return (memb1 + memb2 == 2.0f) ? 1.0f : memb1 + memb2;
        }


        private List<int> GenerateKnots(int degree, int controlSize)
        {
            List<int> knots = new List<int>();
            int i = 0;
            int k = degree + controlSize;
            for (; i < degree; ++i) knots.Add(1);
            for (; i < controlSize; ++i) knots.Add(i - degree + 1);
            for (; i <= k; ++i) knots.Add(controlSize - degree + 1);
            return knots;
        }

        private List<List<List<List<float>>>> GenerateCoef(int controlSizeT, int controlSizeU,
            int offset, int degree,
            List<int> knotsT, List<int> knotsU)
        {
            List<List<List<List<float>>>> res = new List<List<List<List<float>>>>();
            float coef = 0;

            for (int t = 0; t < knotsT[degree + controlSizeT] * offset; ++t)
            {
                List<List<List<float>>> resKnotU = new List<List<List<float>>>();
                for (int u = 0; u < knotsU[degree + controlSizeU] * offset; ++u)
                {
                    List<List<float>> resControlT = new List<List<float>>();
                    for (int controlT = 0; controlT < controlSizeT; ++controlT)
                    {
                        List<float> r = new List<float>();
                        for (int controlU = 0; controlU < controlSizeU; ++controlU)
                        {
                            coef = GenerateN(degree, controlT, knotsT, t * 1.0f / offset)
                            * GenerateN(degree, controlU, knotsU, u * 1.0f / offset);

                            r.Add(coef);
                        }
                        resControlT.Add(r);
                    }
                    resKnotU.Add(resControlT);
                }
                res.Add(resKnotU);
            }

            return res;
        }

        private List<List<Circle>> GenerateRandomControlPoints()
        {
            List<List<Circle>> res = new List<List<Circle>>();
            for (int i = 0; i < controlSizeT; ++i)
            {
                List<Circle> resT = new List<Circle>();
                for (int j = 0; j < controlSizeU; ++j)
                {
                    Vector3 center = new Vector3();
                    center.X = new Random().Next(0, 100) * 0.1f * i + 1;
                    center.Y = new Random().Next(1, 10) * i*j  *0.1f;
                    center.Z = new Random().Next(0, 100) * 0.1f * j   +1;
                    Circle c = new Circle(center);

                    resT.Add(c);
                }
                res.Add(resT);
            }
            return res;
        }
        
    }
}
