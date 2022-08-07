using OpenTK.Graphics.OpenGL4;
using System;

namespace CompGraphEngine.Render.OpenGLAPI
{
    public class VertexBuffer<T> : IDisposable where T :struct
    {
        private readonly int id;
         
        public VertexBuffer(T[] data, int size) 
        {
            

            id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData<T>(BufferTarget.ArrayBuffer,
                size,
                data, BufferUsageHint.StaticDraw);
        }


        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

      

        public void Dispose()
        {
            Bind();
            GL.DeleteBuffer(id);
            UnBind();
        }
    }
}
