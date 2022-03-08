using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public class Triangle2D : PrimitiveObject
    {
        public Triangle2D(float[] _vertices)
        {
            _defaultShader = new Shader("Shaders/triangle.glsl");
           base._vertices = _vertices;
            
        }

        public override void init()
        {
            base.init();
        }

        public override void render()
        {
            base.render();

        }
    }
}
