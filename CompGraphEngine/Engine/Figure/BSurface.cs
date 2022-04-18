using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine.Figure
{
    internal class BSurface: Surface
    {
        List<int> Knots;
        public List<Circle> ControlPoints;
        List<List<float>> coefs;
        int degree = 3, offset = 1000, controlSize = 10;
        public BSurface()
        {
            Transform = new Transform();

            ControlPoints = GenerateRandomControlPoints(controlSize);

            Knots = GenerateKnots(degree, ControlPoints.Count);
            coefs = GenerateCoef(controlSize, offset, degree, Knots);
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
                center.Z = new Random().Next(0, 100) * 0.01f * i;
                Circle c = new Circle(center);

                res.Add(c);
            }
            return res;
        }
    }
}
