using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render
{
    public class VertexBuffer
    {
        private readonly int id;
        public int CountVertex { get; private set; }
        public VertexBuffer( float[] data, int size)
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

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

       
    }
}
