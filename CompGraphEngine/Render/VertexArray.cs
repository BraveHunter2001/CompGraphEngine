using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public class VertexArray
    {
        int id;
        public VertexArray()
        {
            id =  GL.GenVertexArray();
            
        }
        ~VertexArray()
        {
            GL.DeleteVertexArray(id);
        }

        public void AddBuffer(ref VertexBuffer vb,ref  VertexBufferLayout layout)
        {
            Bind();
            vb.Bind();
            int offset = 0;
            
            for(int i = 0; i < layout.Elements.Count; i++)
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

        public void UnBind()
        {
            GL.BindVertexArray(0);
        }
    }
}
