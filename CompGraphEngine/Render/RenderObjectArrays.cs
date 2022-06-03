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
        private PrimitiveType type;
        public RenderObjectArrays(float[,] Points, float[,] Colors, Shader shader, Matrix4 model)
        {
            _vertPoints = Points;
            _vertColors = Colors;
            _shader = shader;
            Model = model;
            type = PrimitiveType.Lines;
        }
        public RenderObjectArrays(float[,] Points, float[,] Colors, Shader shader, Matrix4 model, PrimitiveType type)
        {
            _vertPoints = Points;
            _vertColors = Colors;
            _shader = shader;
            Model = model;
            this.type = type;
        }

        public override void Draw()
        {
            _vertexArray.Bind();
            GL.DrawArrays(type, 0, _pointBuffer.CountVertex);
        }
        
    }
}
