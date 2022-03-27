using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CompGraphEngine.Engine.Figure2D
{
    public class Line : GameObject, IRenderable
    {
        float[,] _vertPoints;
        float[,] _vertColors;

        Color4 color = new Color4(255,255,255,255);
        Shader _shader;

        VertexBuffer _pointBuffer;
        VertexBuffer _colorBuffer;
        VertexArray _vertexArray;

        VertexBufferLayout _layoutPos;
        VertexBufferLayout _layoutCol;
        public Line(float[,] vertPoints, float[,] vertColors) 
        {
            _vertPoints = vertPoints;
            _vertColors = vertColors;

        }


        public Line(Vector3 point1, Vector3 point2)
        {
            _vertPoints = new float[2,3];
            for (int i = 0; i < 3; i++)
            {
                _vertPoints[0, i] = point1[i];
                _vertPoints[1, i] = point2[i];
            }

            _vertColors = new float[2, 4];
            for (int i = 0; i < 4; i++)
            {
                _vertColors[0, i] = ((Vector4)color)[i];
                _vertColors[1, i] = ((Vector4)color)[i];
            }
        }
        public override void Init()
        {
            
            _pointBuffer = new VertexBuffer(Make1DArray(_vertPoints), sizeof(float)* _vertPoints.Length);
            _colorBuffer = new VertexBuffer(Make1DArray(_vertColors), sizeof(float) * _vertColors.Length);
            _vertexArray = new VertexArray();
            _layoutPos = new VertexBufferLayout();
            _layoutCol = new VertexBufferLayout();

            _shader = new Shader("Shaders/line.glsl");

            _layoutPos.Push<float>(_vertPoints.GetLength(1), true);
            _layoutCol.Push<float>(_vertColors.GetLength(1), true);

            _vertexArray.AddLayout(ref _pointBuffer, ref _layoutPos, 0);
            _vertexArray.AddLayout(ref _colorBuffer, ref _layoutCol, 1);
        }

        public void Draw()
        {
            _shader.Use();
            _vertexArray.Bind();
            GL.Enable(EnableCap.LineSmooth);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertPoints.GetLength(0));
            GL.Disable(EnableCap.LineSmooth);
        }
        public override void Update() { }
        

        private float[] Make1DArray(float[,] arr)
        {
            
            float[] res = new float[arr.GetLength(0) * arr.GetLength(1)];
            int sizeArr = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    res[sizeArr++] = arr[i,j];
                }
            }
            return res;
        }

       
    }
}
