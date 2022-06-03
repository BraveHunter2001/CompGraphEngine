using CompGraphEngine.Render;
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
            var indexs = GenerateIndices(); 

            renderObject = new RenderObjectsElements(points, colors,
                new Render.OpenGLAPI.Shader("Shaders/circle.glsl"),
                Transform.Model, indexs);

            renderObject.Init();
        }


        float[,] FillCoordsVertex()
        {
            float[,] _vertPoints = new float[4, 3];

            return _vertPoints;
        }
        float[,] FillColorsVertex()
        {
            //color
            float[,] _vertColors = new float[4, 4];
            
            return _vertColors;
        }
        private int[] GenerateIndices()
        {


            List<int> indexes = new List<int>();

            
           

            return indexes.ToArray();

        }

        public override void Update()
        {
            renderObject.Model = Transform.Model;
        }
    }
}
