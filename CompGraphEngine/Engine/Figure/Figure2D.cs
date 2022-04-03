using CompGraphEngine.Render;

namespace CompGraphEngine.Engine.Figure
{
    public class Figure2D : GameObject
    {

        protected float[,] _vertPoints;
        protected float[,] _vertColors;

        protected Shader _shader;

        protected VertexBuffer _pointBuffer;
        protected VertexBuffer _colorBuffer;

        protected VertexArray _vertexArray;

        protected VertexBufferLayout _layoutPos;
        protected VertexBufferLayout _layoutCol;


        public override void Init()
        {
            _pointBuffer = new VertexBuffer(Make1DArray(_vertPoints), sizeof(float) * _vertPoints.Length);
            _colorBuffer = new VertexBuffer(Make1DArray(_vertColors), sizeof(float) * _vertColors.Length);
            _vertexArray = new VertexArray();
            _layoutPos = new VertexBufferLayout();
            _layoutCol = new VertexBufferLayout();
          



            _layoutPos.Push<float>(_vertPoints.GetLength(1), true);
            _layoutCol.Push<float>(_vertColors.GetLength(1), true);

            _vertexArray.AddLayout(ref _pointBuffer, ref _layoutPos, 0);
            _vertexArray.AddLayout(ref _colorBuffer, ref _layoutCol, 1);
            IsInited = true;
        }

        public override void Update()
        {
           // _pointBuffer.BufferSubData(Make1DArray(_vertPoints));
            _pointBuffer.BufferSubData(Make1DArray(_vertColors));
        }

        private float[] Make1DArray(float[,] arr)
        {

            float[] res = new float[arr.GetLength(0) * arr.GetLength(1)];
            int sizeArr = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    res[sizeArr++] = arr[i, j];
                }
            }
            return res;
        }



    }
}
