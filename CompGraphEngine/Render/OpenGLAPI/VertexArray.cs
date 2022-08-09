using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render.OpenGLAPI
{
    public class VertexArray: IDisposable 
    {
        private readonly int id;
        public VertexArray()
        {
            id = GL.GenVertexArray();

        }
        
        /// <summary>
        /// Add layout into buffer without offset
        /// </summary>
        /// <param name="layout"></param>
        public void AddLayout(ref VertexBuffer vb, ref VertexBufferLayout layout, int index)
        {
            Bind();
            vb.Bind();

            var elem = layout.Elements[0];
            GL.VertexAttribPointer(index, elem.count, elem.type, elem.normalized, 0, 0);
            GL.EnableVertexAttribArray(index);


            vb.UnBind();
            UnBind();
        }

        public void AddLayouts(ref VertexBuffer vb, ref VertexBufferLayout layout)
        {
            Bind();
            vb.Bind();
            int offset = 0;

            for (int i = 0; i < layout.Elements.Count; i++)
            {
                var elem = layout.Elements[i];
                GL.VertexAttribPointer(i, elem.count, elem.type, elem.normalized, layout.Stride, offset);
                GL.EnableVertexAttribArray(i);
                offset += elem.count * VertexBufferLayout.GetSizeOpenGLType(elem.type);
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(id);
        }

        public void Dispose()
        {
            Bind();
            GL.DeleteVertexArray(id);
            UnBind();
        }

        public void UnBind()
        {
            GL.BindVertexArray(0);
        }
    }
}
