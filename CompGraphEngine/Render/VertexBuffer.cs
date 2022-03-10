using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render
{
    public class VertexBuffer<T> where T : struct
    {
        private int id;

        public VertexBuffer(T[] data, int size)
        {
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
