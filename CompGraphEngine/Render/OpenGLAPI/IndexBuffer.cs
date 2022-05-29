using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render.OpenGLAPI
{
    public class IndexBuffer
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
    }
}
