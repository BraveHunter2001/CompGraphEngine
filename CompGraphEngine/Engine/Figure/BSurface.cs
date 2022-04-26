using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Color4 color = Color4.White;

        List<int> KnotsT;
        List<int> KnotsU;

        public readonly List<List<Circle>> ControlPoints;


        private List<List<List<List<float>>>> coefs;


        private int degree = 1, controlSizeT = 8, controlSizeU = 4, offset = 0;
        private int shiftT = 0, shiftU = 0;
        public readonly ulong CountPoligons;

        public BSurface(int degree,int offest,  List<List<Circle>> ControlPoints)
        {
            Stopwatch sp = new Stopwatch();

            Transform = new Transform();

            this.degree = degree;
            this.controlSizeT = ControlPoints.Count;
            this.controlSizeU = ControlPoints[0].Count;

            this.offset = offest;

            this.ControlPoints = ControlPoints;

            KnotsT = GenerateKnots(degree, controlSizeT);
            KnotsU = GenerateKnots(degree, controlSizeU);

            shiftT = KnotsT[0] * offset;
            shiftU = KnotsU[0] * offset;
         

            FillCoordsVertex();
            FillColorsVertex();
            GenerateIndices(KnotsT[degree + controlSizeT] * offset - shiftT, KnotsU[degree + controlSizeU] * offset - shiftU);
            CountPoligons = (ulong)((KnotsT[degree + controlSizeT] * offset - shiftT - 1) * (KnotsU[degree + controlSizeU] * offset - shiftU - 1)) * 2;


        }

        public BSurface(int degree, int offset, int controlSizeT, int controlSizeU)
        {
            Stopwatch sp = new Stopwatch();
            Transform = new Transform();
            this.degree = degree;
            this.controlSizeT = controlSizeT;
            this.controlSizeU = controlSizeU;

            this.offset = offset;
            sp.Start();
            ControlPoints = GenerateRandomControlPoints();

            KnotsT = GenerateKnots(degree, controlSizeT);
            KnotsU = GenerateKnots(degree, controlSizeU);

            shiftT = KnotsT[0] * offset;
            shiftU = KnotsU[0] * offset;

            
          

            FillCoordsVertex();
            FillColorsVertex();
            GenerateIndices(KnotsT[degree + controlSizeT] * offset - shiftT, KnotsU[degree + controlSizeU] * offset - shiftU);

            CountPoligons = (ulong)((KnotsT[degree + controlSizeT] * offset - shiftT - 1) * (KnotsU[degree + controlSizeU] * offset - shiftU - 1)) * 2;
            sp.Stop();
            Console.WriteLine($"Generate time Surface {sp.Elapsed.TotalSeconds}");
            Console.WriteLine($"Count poliggons: {CountPoligons}");
        }

        public override void Init()
        {
            
            _indexBuffer = new IndexBuffer(_indexes, _indexes.Length);
            _shader = new Shader("Shaders/surface.glsl");

            _indexes = null;
            KnotsT = null;
            KnotsU = null;
            coefs = null;
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
            for (int i = 0; i < row-1; i++)
            {
                for (int j = 0; j < col-1; j++)
                {
                    indexes.Add(i * col + j);
                    indexes.Add(i * col + (j + 1));
                    indexes.Add((i + 1) * col + j);

                    indexes.Add((i + 1) * col + j);
                    indexes.Add(i * col + (j + 1));
                    indexes.Add((i + 1) * col + (j + 1));


                    
                }
            }

            _indexes = indexes.ToArray();

        }
        public void updateTime(float dt)
        {
            _shader.SetFloat("vTime", dt);

        }
        void FillCoordsVertex()
        {
            int size = (KnotsT[degree + controlSizeT] * offset - shiftT)
                * (KnotsU[degree + controlSizeU] * offset -shiftU);
            _vertPoints = new float[size, 3];
            int shift = 0;
            float coef = 0;
            long p = 0;
            for (int t = 0; t < KnotsT[degree + controlSizeT] * offset - shiftT; t++)
            {
                for (int u = 0; u < KnotsU[degree + controlSizeU] * offset - shiftU; u++)
                {
                    for (int controlT = 0; controlT < controlSizeT; controlT++)
                    {
                        for (int controlU = 0; controlU < controlSizeU; controlU++)
                        {
                            coef= GenerateN(degree, controlT, KnotsT, (t + shiftT) * 1.0f / offset)
                            * GenerateN(degree, controlU, KnotsU, (u + shiftU)* 1.0f / offset);

                            _vertPoints[shift, 0] += ControlPoints[controlT][controlU].Transform.Position.X
                                * coef;
                            _vertPoints[shift, 1] += ControlPoints[controlT][controlU].Transform.Position.Y 
                                * coef;
                            _vertPoints[shift, 2] += ControlPoints[controlT][controlU].Transform.Position.Z 
                                * coef;
                            p++;
                            
                        }
                    }
                    Console.WriteLine($"Calc coef ({coef})| {p}/{size * controlSizeT * controlSizeU} ");
                    shift++;
                }
               
            }

        }
        void FillColorsVertex()
        {
            int size = (KnotsT[degree + controlSizeT] * offset - shiftT)
                * (KnotsU[degree + controlSizeU] * offset - shiftU);
            _vertColors = new float[size, 4];
            int count = 0;

            for (int i = 0; i < KnotsT[degree + controlSizeT] * offset - shiftT; i++)
            {
                for (int j = 0; j < KnotsU[degree + controlSizeU] * offset - shiftU; j++)
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

            for (int t = shiftT; t < knotsT[degree + controlSizeT] * offset; ++t)
            {
                List<List<List<float>>> resKnotU = new List<List<List<float>>>();
                for (int u = shiftU; u < knotsU[degree + controlSizeU] * offset; ++u)
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

        public List<List<Circle>> GenerateRandomControlPoints()
        {
            List<List<Circle>> res = new List<List<Circle>>();
            for (int i = 0; i < controlSizeT; ++i)
            {
                List<Circle> resT = new List<Circle>();
                for (int j = 0; j < controlSizeU; ++j)
                {
                    Vector3 center = new Vector3();
                    center.X =(j +1) * 5;
                    center.Y = new Random().Next(-10, 10) ;
                    center.Z =(i+1) * 5;
                    Circle c = new Circle(center);

                    resT.Add(c);
                }
                res.Add(resT);
            }
            return res;
        }

        public static List<List<Circle>> GeneratedPolygon(int controlSizeT, int controlSizeU)
        {
            List<List<Circle>> res = new List<List<Circle>>();

            
            for (int i = 0; i < controlSizeT; ++i)
            {
                List<Circle> resT = new List<Circle>();
                for (int j = 0; j < controlSizeU; ++j)
                {
                    Vector3 center = new Vector3();
                    center.X = (j) * 5;
                    center.Y = (float)MathHelper.Sin(i)  * 5;
                    center.Z = (float)MathHelper.Cos(i) * 5;
                    Circle c = new Circle(center);

                    resT.Add(c);
                }
                res.Add(resT);
            }
            return res;
        }
    }
}
