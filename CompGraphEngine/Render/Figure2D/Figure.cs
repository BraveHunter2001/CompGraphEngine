using CompGraphEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public abstract class Figure
    {
        private protected VertexArray _vertexArray;
        private protected VertexBuffer _vertexBuffer;
        private protected IndexBuffer _indexBuffer;
        private protected VertexBufferLayout _layout;
        private protected Shader _shader;
        private protected int _countVertex;

        protected float[] _vertices; 

        
        public virtual void Init()
        {
            _vertexBuffer = new VertexBuffer(_vertices, _vertices.Length * sizeof(float));
            _vertexArray = new VertexArray();
            _layout = new VertexBufferLayout();
        }

        protected float[] toArrayFromPoints(float[][] posVerts, Vector4f color, int sizePosition, int sizeColor)
        {
            float[] vert = new float[(sizePosition + sizeColor) * 3];

            int shift = 0;
            for (int i = 0; i <_countVertex; i++)
            {

                for (int j = 0; j < sizePosition; j++)
                {
                    vert[shift] = posVerts[i][j];
                    shift++;
                }
                shift += sizeColor;
                int s = (sizePosition + sizeColor) * i;

                for (int j = 0; j < sizeColor; j++)
                {
                    int curr = (sizeColor - 1 + j);
                    vert[curr + s] = color.ToArray()[j];
                }

            }

            return vert;
        }
        internal virtual void Draw() { }

    }
}
