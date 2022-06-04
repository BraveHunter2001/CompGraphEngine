using OpenTK.Graphics.OpenGL4;
using System;

namespace CompGraphEngine.Render.OpenGLAPI
{
    public class IndexBuffer : IDisposable
    {
        private readonly int id;
        private readonly int count = 0;
        internal IndexBuffer(int[] data, int count)
        {
            this.count = count;
            id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                count * sizeof(int),
                data, BufferUsageHint.StaticDraw);
        }
        

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public int GetId() => id;

        public int GetCount() => count;

        public void Dispose()
        {
            Bind();
            GL.DeleteBuffer(id);
            UnBind();
        }
    }
}
