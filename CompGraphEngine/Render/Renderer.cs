using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public class Renderer
    {
        internal void Draw(ref VertexArray va, ref VertexBuffer vb, ref Shader shader)
        {
            shader.Use();
            va.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, vb.CountVertex);
        }

        internal void Draw(ref VertexArray va, ref VertexBuffer vb, ref  IndexBuffer ib, ref Shader shader)
        {
            shader.Use();
            va.Bind();
            ib.Bind();

            GL.DrawElements(PrimitiveType.Triangles, ib.GetCount(),DrawElementsType.UnsignedInt, 0);
        }

        public void Draw(Figure figure)
        {
            figure.Draw();
        }

     
    }
}
