using CompGraphEngine.Render;
using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CompGraphEngine.Engine.Figure
{
    internal class BSurface : GameObject
    {
        
        Color4 color = Color4.White;

        List<int> KnotsT;
        List<int> KnotsU;

        public readonly List<List<Circle>> ControlPoints;
        public Shader sh = new Shader("Shaders/surfaceWithDepth.glsl");

        private int degreeT = 1, degreeU = 1, controlSizeT = 8, controlSizeU = 4, offset = 0;
        private int shiftT = 0, shiftU = 0;
       

        public BSurface(int degree, int offest, List<List<Circle>> ControlPoints)
        {
            

            Transform = new Transform();

            this.degreeT = this.degreeU  = degree;
            this.controlSizeT = ControlPoints.Count;
            this.controlSizeU = ControlPoints[0].Count;

            this.offset = offest;

            this.ControlPoints = ControlPoints;



        }
        public BSurface(int degree, int offset, int controlSizeT, int controlSizeU)
        {
           
            Transform = new Transform();
            this.degreeT = this.degreeU = degree;
            this.controlSizeT = controlSizeT;
            this.controlSizeU = controlSizeU;

            this.offset = offset;
        
            ControlPoints = GenerateRandomControlPoints();

        }

        public BSurface(int degreeT,int degreeU, int offest, List<List<Circle>> ControlPoints)
        {
           

            Transform = new Transform();

            this.degreeT = degreeT;
            this.degreeU = degreeU;

            this.controlSizeT = ControlPoints.Count;
            this.controlSizeU = ControlPoints[0].Count;

            this.offset = offest;

            this.ControlPoints = ControlPoints;

           


        }
        public BSurface(int degreeT, int degreeU, int offset, int controlSizeT, int controlSizeU)
        {
           
            Transform = new Transform();

            this.degreeT = degreeT;
            this.degreeU = degreeU;

            this.controlSizeT = controlSizeT;
            this.controlSizeU = controlSizeU;

            this.offset = offset;
            
            ControlPoints = GenerateRandomControlPoints();

        }


        

        public override void Init()
        {
            if (degreeT > controlSizeT || degreeU > controlSizeU)
                throw new ArgumentException("Степень больше контрольных точек");

            KnotsT = GenerateKnots(degreeT, controlSizeT);
            KnotsU = GenerateKnots(degreeU, controlSizeU);

            shiftT = KnotsT[0] * offset;
            shiftU = KnotsU[0] * offset;


            var points = FillCoordsVertex();
            var colors = FillColorsVertex();
            var indeces = GenerateIndices(KnotsT[degreeT + controlSizeT] * offset - shiftT + 1, KnotsU[degreeU + controlSizeU] * offset - shiftU + 1);
           

            
            

            
            KnotsT = null;
            KnotsU = null;

           

            renderObject = new RenderObjectsElements(points, colors,
                sh,
                Transform.Model,
                indeces);

            renderObject.Init();
        }


      
        
        private int[] GenerateIndices(int row, int col)
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



                }
            }

            return indexes.ToArray();

        }

        float[,]  FillCoordsVertex()
        {
            int size = (KnotsT[degreeT + controlSizeT] * offset - shiftT + 1)
                * (KnotsU[degreeU + controlSizeU] * offset - shiftU + 1);
            float[,] _vertPoints = new float[size, 3];
            int shift = 0;
            float coef = 0;
           // int p = 0;
            for (int t = 0; t < KnotsT[degreeT + controlSizeT] * offset - shiftT + 1; t++)
            {
                for (int u = 0; u < KnotsU[degreeU + controlSizeU] * offset - shiftU + 1; u++)
                {
                    for (int controlT = 0; controlT < controlSizeT; controlT++)
                    {
                        for (int controlU = 0; controlU < controlSizeU; controlU++)
                        {
                            coef = GenerateN(degreeT, controlT, KnotsT, (t + shiftT) * 1.0f / offset)
                            * GenerateN(degreeU, controlU, KnotsU, (u + shiftU) * 1.0f / offset);

                            _vertPoints[shift, 0] += ControlPoints[controlT][controlU].Transform.Position.X
                                * coef;
                            _vertPoints[shift, 1] += ControlPoints[controlT][controlU].Transform.Position.Y
                                * coef;
                            _vertPoints[shift, 2] += ControlPoints[controlT][controlU].Transform.Position.Z
                                * coef;
                            //p++;
                        }
                    }
                     //Console.WriteLine($"Calc coef ({coef})| {p}/{size * controlSizeT * controlSizeU} ");
                    shift++;
                }

            }
            return _vertPoints;
        }
        float[,]  FillColorsVertex()
        {
            int size = (KnotsT[degreeT + controlSizeT] * offset - shiftT + 1)
                * (KnotsU[degreeU + controlSizeU] * offset - shiftU + 1);
            float[,] _vertColors = new float[size, 4];
            int count = 0;

            for (int i = 0; i < KnotsT[degreeT + controlSizeT] * offset - shiftT + 1; i++)
            {
                for (int j = 0; j < KnotsU[degreeU + controlSizeU] * offset - shiftU + 1; j++)
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
                    center.X = (j + 1) * 5;
                    center.Y = new Random().Next(-20, 200) * 0.1f;
                    center.Z = (i + 1) * 5;
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
            List<Circle> first = new List<Circle>();

            for (int i = 0; i < controlSizeT - 1; ++i)
            {

                List<Circle> resT = new List<Circle>();
                if (i == 0)
                    first = resT;
                for (int j = 0; j < controlSizeU; ++j)
                {
                    Vector3 center = new Vector3();
                    center.X = (j) * 5;
                    center.Y = (float)MathHelper.Sin(i) * 5 + new Random().Next(-10, 10) * 0.1f;
                    center.Z = (float)MathHelper.Cos(i) * 5 + new Random().Next(-10, 10) * 0.1f;
                    Circle c = new Circle(center);

                    resT.Add(c);
                }
                res.Add(resT);
            }
            res.Add(first);
            return res;
        }

        public static List<List<Circle>> GeneratedPolygonByX(List<Circle> ControlPointPolygon,int countPolygon, float shiftX)
        {
            List<List<Circle>> res = new List<List<Circle>>();
            float shift = 0;
            for (int i = 0; i < countPolygon; ++i)
            {
                List<Circle> resT = new List<Circle>();
                
                for (int j = 0; j < ControlPointPolygon.Count; ++j)
                {
                    Vector3 center = new Vector3();
                    center.X = ControlPointPolygon[j].Transform.Position.X + shift;
                    center.Y = ControlPointPolygon[j].Transform.Position.Y;
                    center.Z = ControlPointPolygon[j].Transform.Position.Z;
                    Circle c = new Circle(center);

                    resT.Add(c);
                }
                shift += shiftX;
                res.Add(resT);
            }
            return res;
        }
        public static List<List<Circle>> GeneratedPolygonByY(List<Circle> ControlPointPolygon, int countPolygon, float shiftY)
        {
            List<List<Circle>> res = new List<List<Circle>>();
            float shift = 0;
            for (int i = 0; i < countPolygon; ++i)
            {
                List<Circle> resT = new List<Circle>();

                for (int j = 0; j < ControlPointPolygon.Count; ++j)
                {
                    Vector3 center = new Vector3();
                    center.X = ControlPointPolygon[j].Transform.Position.X ;
                    center.Y = ControlPointPolygon[j].Transform.Position.Y + shift;
                    center.Z = ControlPointPolygon[j].Transform.Position.Z;
                    Circle c = new Circle(center);

                    resT.Add(c);
                }
                shift += shiftY;
                res.Add(resT);
            }
            return res;
        }
        public static List<List<Circle>> GeneratedPolygonByZ(List<Circle> ControlPointPolygon, int countPolygon, float shiftZ)
        {
            List<List<Circle>> res = new List<List<Circle>>();
            float shift = 0;
            for (int i = 0; i < countPolygon; ++i)
            {
                List<Circle> resT = new List<Circle>();

                for (int j = 0; j < ControlPointPolygon.Count; ++j)
                {
                    Vector3 center = new Vector3();
                    center.X = ControlPointPolygon[j].Transform.Position.X;
                    center.Y = ControlPointPolygon[j].Transform.Position.Y;
                    center.Z = ControlPointPolygon[j].Transform.Position.Z + shift;
                    Circle c = new Circle(center);

                    resT.Add(c);
                }
                shift += shiftZ;
                res.Add(resT);
            }
            return res;
        }

        public override void Update()
        {
            renderObject.Model = Transform.Model;
        }
    }
}
