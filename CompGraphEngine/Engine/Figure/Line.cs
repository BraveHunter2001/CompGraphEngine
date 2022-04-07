using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CompGraphEngine.Engine.Figure

{
    public class Line : Figure, IRenderable
    {
        private Vector3 point1;
        private Vector3 point2;
        Matrix4 MVP;
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
            FillCoordsVertex();
            FillColorsVertex();

            

            _shader = new Shader("Shaders/line.glsl");
            
            
            base.Init();
        }

        void FillCoordsVertex()
        {
            _vertPoints = new float[2, 3];
            for (int i = 0; i < 3; i++)
            {
                _vertPoints[0, i] = point1[i];
                _vertPoints[1, i] = point2[i];
            }

        }

        void FillColorsVertex()
        {
            _vertColors = new float[2, 4];
            for (int i = 0; i < 4; i++)
            {
                _vertColors[0, i] = ((Vector4)color)[i];
                _vertColors[1, i] = ((Vector4)color)[i];
            }
        }

        public void Draw(Camera camera) // todo delete this shit
        {

            MVP = camera.GetProjection() * camera.GetViewMatrix() * Transform.Model;
            _shader.SetMatrix4("aMVP", MVP);

            _shader.Use();
            _vertexArray.Bind();

          GL.Enable(EnableCap.LineSmooth);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertPoints.GetLength(0));
           GL.Disable(EnableCap.LineSmooth);
            
        }
        public override void Update()
        {
            //Update points
            for (int i = 0; i < 3; i++)
            {
                _vertPoints[0, i] = point1[i];
                _vertPoints[1, i] = point2[i];
            }
            
            

            base.Update(); 
        }



    }
}
