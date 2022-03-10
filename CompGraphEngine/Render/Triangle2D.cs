using CompGraphEngine.Scene;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public class Triangle2D : PrimitiveObject
    {
      

        public Triangle2D(float[] vertices) : base(3, 4 , vertices, new Shader("Shaders/triangle.glsl"))
        {
            


        }

    }
}
