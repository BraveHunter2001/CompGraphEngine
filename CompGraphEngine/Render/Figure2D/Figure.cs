using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public abstract class Figure
    {
        private protected  VertexArray _vertexArray;
        private protected VertexBuffer _vertexBuffer;
        private protected IndexBuffer _indexBuffer;
        private protected VertexBufferLayout _layout;
        private protected Shader _shader;

        protected float[] _vertices; 

        public virtual void Init()
        {
            _vertexBuffer = new VertexBuffer(_vertices, _vertices.Length * sizeof(float));
            _vertexArray = new VertexArray();
            _layout = new VertexBufferLayout();
        }

        internal virtual void Draw() { }
        
     
    }
}
