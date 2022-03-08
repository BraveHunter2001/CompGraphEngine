using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public abstract class PrimitiveObject
    {
        private const int POSITION_SIZE = 3; // x y z
        private const int COLOR_SIZE = 4; // r g b a

        private const int POSITION_OFFSET = 0;
        private const int COLOR_OFFSET = POSITION_OFFSET + POSITION_SIZE * sizeof(float);
        private const int STRIDE = POSITION_SIZE + COLOR_SIZE;

        private const int STRIDE_SIZE = ( POSITION_SIZE + COLOR_SIZE) * sizeof(float);


        protected float[] _vertices;
        //protected uint[] _indices;
        
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        //private int _elementBufferObject;

        private int countVertexes = 0;
        protected Shader _defaultShader;
      

        public virtual void init()
        {
            VBOInit();
            VAOInit();

            _defaultShader.Use();
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
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(_defaultShader.Handle);
        }

        private void VBOInit()
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw);
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
