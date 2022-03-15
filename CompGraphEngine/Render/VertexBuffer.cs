using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render
{
    internal class VertexBuffer
    {
        private readonly int id;
        internal int CountVertex { get; private set; }
        internal VertexBuffer(float[] data, int size)
        {
            CountVertex = data.GetLength(0);

            id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData(BufferTarget.ArrayBuffer,
                size,
                data, BufferUsageHint.StaticDraw);
        }
        ~VertexBuffer()
        {
            GL.DeleteBuffer(id);
        }

        internal void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        }

        internal void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

       
    }
}
