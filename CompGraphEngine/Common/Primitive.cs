using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Common
{
    public abstract class Primitive
    {
        private readonly float[] _vertices;
        private readonly uint[] _indices;

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;

        private Shader _defaultShader;

        
    }
}
