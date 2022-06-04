using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace CompGraphEngine.Engine.Figure
{
    internal class BSpline :GameObject
    {

        
        Color4 color = Color4.White;

        List<int> Knots;
        public List<Circle> ControlPoints;
        List<List<float>> coefs;

        int degree = 3, offset= 1000, controlSize = 5;

        public BSpline()
        {
            Transform = new Transform();

            ControlPoints = GenerateRandomControlPoints(controlSize);

            Knots = GenerateKnots(degree, ControlPoints.Count);
            coefs = GenerateCoef(controlSize, offset, degree, Knots);
        }

        public override void Init()
        {
            

            var points = FillCoordsVertex(degree, controlSize, offset);
            var colors = FillColorsVertex(Knots[degree + controlSize] * offset);

            renderObject = new RenderObjectArrays(points, colors,
                new Render.OpenGLAPI.Shader("Shaders/line.glsl"),
                Transform.Model);

            ControlPoints = null;
            Knots = null;
            coefs = null;
            GC.Collect();
            renderObject.Init();
        }



        float[,] FillCoordsVertex(int degree, int controlSize, int offset)
        {

            float[,] _vertPoints = new float[Knots[degree + controlSize] * offset, 3];

            for (int t = 0; t < Knots[degree + controlSize] * offset; t++)
            {
                for (int control = 0; control < controlSize; control++)
                {
                    _vertPoints[t, 0] += ControlPoints[control].Transform.Position.X * coefs[t][control];
                    _vertPoints[t, 1] += ControlPoints[control].Transform.Position.Y * coefs[t][control];
                    _vertPoints[t, 2] += ControlPoints[control].Transform.Position.Z * coefs[t][control];
                }

            }

            return _vertPoints;

        }
        float[,] FillColorsVertex(int size)
        {

            float[,] _vertColors = new float[size, 4];
           

            for (int i = 0; i < size; i++)
            {
               
                    _vertColors[i, 0] = color.R;
                    _vertColors[i, 1] = color.G;
                    _vertColors[i, 2] = color.B;
                    _vertColors[i, 3] = color.A;
                   
                

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

        private List<List<float>> GenerateCoef(int controlSize, int offset, int degree, List<int> knots)
        {
            List<List<float>> res = new List<List<float>>();

            for (int t = 0; t < knots[degree + controlSize] * offset; ++t)
            {
                List<float> coefForControls = new List<float>();
                for (int control = 0; control < controlSize; ++control)
                {
                    coefForControls.Add(GenerateN(degree, control, knots, t * 1.0f / offset));
                }
                res.Add(coefForControls);
            }

            return res;
        }

        private List<Circle> GenerateRandomControlPoints(int controlSize)
        {
            List<Circle> res = new List<Circle>();
            for (int i = 0; i < controlSize; ++i)
            {
                Vector3 center = new Vector3();
                center.X = new Random().Next(0, 100) * 0.01f * i;
                center.Y = new Random().Next(0, 100) * 0.01f * i;
                center.Z = 0;
                Circle c = new Circle(center);
                
                res.Add(c);
            }
            return res;
        }

        public override void Update()
        {
            renderObject.Model = Transform.Model;
        }
    }
}
