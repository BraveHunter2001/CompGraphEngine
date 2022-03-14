using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render
{
    public abstract class PrimitiveObject
    {
       


        protected float[] _vertices;
        //protected uint[] _indices;

        protected VertexBuffer _vertexBuffer;

        protected VertexArray _vertexArray;
        //private int _elementBufferObject;

        private int countVertexes = 0;
        private Shader _defaultShader;

        private VertexBufferLayout _layout;
        
        public PrimitiveObject(int positionSize, int colorSize, float[] verts, Shader shader)
        {

            _vertices = verts;
            _defaultShader = shader;
            countVertexes = CountVertexes(positionSize, colorSize);

            _vertexBuffer = new VertexBuffer(_vertices, _vertices.Length * sizeof(float));
            _vertexArray = new VertexArray();
            
            _layout = new VertexBufferLayout();
            _layout.Push<float>(positionSize);
            _layout.Push<float>(colorSize);

            _vertexArray.AddBuffer(ref _vertexBuffer, ref _layout);

            _defaultShader.Use();

            

        }
        ~PrimitiveObject()
        {
            unload();
        }

       
        public virtual void render()
        {
            _defaultShader.Use();
            _vertexArray.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, countVertexes);
        }
        public virtual void unload()
        {
            _vertexBuffer.UnBind();
           _vertexArray.UnBind();
            
        }

        private int CountVertexes(params int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
                   sum = sum + numbers[i];
            return sum;
        }

    }
}
