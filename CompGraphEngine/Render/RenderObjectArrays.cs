using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public class RenderObjectArrays : RenderObject
    {
        public RenderObjectArrays(float[,] Points, float[,] Colors, Shader shader, Matrix4 model)
        {
            _vertPoints = Points;
            _vertColors = Colors;
            _shader = shader;
            Model = model;
        }

        public override void Draw()
        {
            _vertexArray.Bind();
            GL.DrawArrays(PrimitiveType.Lines, 0, _pointBuffer.CountVertex);
        }
        
    }
}
