using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render.Model
{
    struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TexCoords;

        unsafe public static int Size() => sizeof(Vector3) * 2 + sizeof(Vector2);
    }
}
