using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render.OpenGLAPI
{
    internal class IndexBuffer
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
        ~IndexBuffer()
        {
            GL.DeleteBuffer(id);
        }

        internal void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
        }

        internal void UnBind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        internal int GetId() => id;

        internal int GetCount() => count;
    }
}
