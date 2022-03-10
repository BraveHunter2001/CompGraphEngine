using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render
{
    public abstract class PrimitiveObject
    {
        private int POSITION_SIZE;
        private int COLOR_SIZE;

        private const int POSITION_OFFSET = 0;
        private int COLOR_OFFSET;
        private int STRIDE;
        private int STRIDE_SIZE;


        protected float[] _vertices;
        //protected uint[] _indices;

        protected VertexBuffer<float> _vertexBuffer;

        protected int _vertexArrayObject;
        //private int _elementBufferObject;

        private int countVertexes = 0;
        private Shader _defaultShader;


        public PrimitiveObject(int positionSize, int colorSize, float[] verts, Shader shader)
        {
            POSITION_SIZE = positionSize;
            COLOR_SIZE = colorSize;
            _vertices = verts;

            COLOR_OFFSET = POSITION_OFFSET + POSITION_SIZE * sizeof(float);
            STRIDE = POSITION_SIZE + COLOR_SIZE;
            STRIDE_SIZE = (POSITION_SIZE + COLOR_SIZE) * sizeof(float);

            _defaultShader = shader;


            _vertexBuffer = new VertexBuffer<float>(_vertices, _vertices.Length * sizeof(float));
            _defaultShader.Use();

            init();

        }
        ~PrimitiveObject()
        {
            unload();
        }

        public virtual void init()
        {

            VAOInit();

            countVertexes = _vertices.Length / STRIDE;
        }
        public virtual void render()
        {
            _defaultShader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, countVertexes);
        }
        public virtual void unload()
        {
            _vertexBuffer.UnBind();
            GL.DeleteVertexArray(_vertexArrayObject);
            
        }

        

        private void VAOInit()
        {
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            VAOPositionInit();
            VAOColorInit();
        }

        private void VAOPositionInit()
        {
            GL.VertexAttribPointer(0, POSITION_SIZE, VertexAttribPointerType.Float, false, STRIDE_SIZE, POSITION_OFFSET);
            GL.EnableVertexAttribArray(0);
        }
        private void VAOColorInit()
        {
            GL.VertexAttribPointer(1, COLOR_SIZE, VertexAttribPointerType.Float, false, STRIDE_SIZE, COLOR_OFFSET);
            GL.EnableVertexAttribArray(1);
        }
    }
}
