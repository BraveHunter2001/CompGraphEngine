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
    public class Poligon : GameObject
    {
        Color4 color = Color4.Blue;
        List<Vector3> ControlPoints; 
        public Poligon(List<Vector3> controlPoints)
        {
            Transform = new Transform();
            ControlPoints = controlPoints;
        }
        public override void Init()
        {
            var points = FillCoordsVertex();
            var colors = FillColorsVertex();
            var indexes = GenerateIndices(); 

            renderObject = new RenderObjectsElements(points, colors,
                new Render.OpenGLAPI.Shader("Shaders/rectangle.glsl"),
                Transform.Model, indexes);

            renderObject.Init();
        }

        public Poligon(List<Vector3> controlPoints, Color4 color)
        {
            Transform = new Transform();
            ControlPoints = controlPoints;
            this.color = color;
        }

        float[,] FillCoordsVertex()
        {
            float[,] _vertPoints = new float[ControlPoints.Count, 3];
            for(int i = 0; i < ControlPoints.Count; i++)
            {
                _vertPoints[i, 0] = ControlPoints[i].X;
                _vertPoints[i, 1] = ControlPoints[i].Y;
                _vertPoints[i, 2] = ControlPoints[i].Z;
            }

            return _vertPoints;
        }
        float[,] FillColorsVertex()
        {
            //color
            float[,] _vertColors = new float[ControlPoints.Count, 4];
            for (int i = 0; i < ControlPoints.Count; i++)
            {
                _vertColors[i, 0] = color.R;
                _vertColors[i, 1] = color.G;
                _vertColors[i, 2] = color.B;
                _vertColors[i, 3] = 0.5f;
            }
            
            return _vertColors;
        }
        private int[] GenerateIndices()
        {
            int current = 0;
            List<int> indexes = new List<int>();
            for (int i = 0; i < ControlPoints.Count - 2; i++)
            {
                indexes.Add(0);
                indexes.Add(current + 1);
                indexes.Add(current + 2);
                current++;
            }
            
           

            return indexes.ToArray();

        }

        public override void Update()
        {
            renderObject.Model = Transform.Model;
        }
    }
}
