using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render
{
    internal class IndexBuffer
    {
        private int id;
        private int count;
        public IndexBuffer(int[] data, int count)
        {
            this.count = count;
            id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                count  * sizeof(int),
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

        public int GetCount() => count;
    }
}
