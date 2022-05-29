using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CompGraphEngine.Engine.Figure

{
    public class Line : GameObject
    {
        private Vector3 point1;
        private Vector3 point2;

        Color4 color = new Color4(255, 255, 255, 255);
        public Vector3 Point1
        {
            get
            {
                return point1;
            }

            set
            {
                point1 = value;

            }
        }
        public Vector3 Point2
        {
            get
            {
                return point2;
            }

            set
            {
                point2 = value;


            }
        }

        public Line(Vector3 point1, Vector3 point2)
        {
            Transform = new Transform();
            this.point1 = point1;
            this.point2 = point2;

        }
        public Line(Vector3 point1, Vector3 point2, Color4 color)
        {
            Transform = new Transform();
            this.point1 = point1;
            this.point2 = point2;
            this.color = color;
        }
        public override void Init()
        {
           var points = FillCoordsVertex();
           var colors = FillColorsVertex();

            renderObject = new RenderObjectArrays(points, colors,
                new Render.OpenGLAPI.Shader("Shaders/line.glsl"),
                Transform.Model);

            renderObject.Init();
        }

        float[,] FillCoordsVertex()
        {
            float[,] _vertPoints = new float[2, 3];
            for (int i = 0; i < 3; i++)
            {
                _vertPoints[0, i] = point1[i];
                _vertPoints[1, i] = point2[i];
            }
            return _vertPoints;
        }

        float[,] FillColorsVertex()
        {
            float[,] _vertColors = new float[2, 4];
            for (int i = 0; i < 4; i++)
            {
                _vertColors[0, i] = ((Vector4)color)[i];
                _vertColors[1, i] = ((Vector4)color)[i];
            }
            return _vertColors;
        }

      

        public override void Update()
        {

        }
    }
}
