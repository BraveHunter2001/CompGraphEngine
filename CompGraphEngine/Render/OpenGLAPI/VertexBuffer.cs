using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render.OpenGLAPI
{
    public class VertexBuffer
    {
        private readonly int id;
        public int CountVertex { get; private set; }
        public VertexBuffer(float[] data, int size)
        {
            CountVertex = data.Length;

            id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData(BufferTarget.ArrayBuffer,
                size,
                data, BufferUsageHint.DynamicDraw);
        }
        ~VertexBuffer()
        {
            GL.DeleteBuffer(id);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void BufferSubData(float[] data)
        {
            Bind();
            GL.BufferSubData(BufferTarget.ArrayBuffer, System.IntPtr.Zero, data.Length * sizeof(float), data);
            UnBind();
        }
    }
}
