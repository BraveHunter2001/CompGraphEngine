using CompGraphEngine.Render;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine.Figure
{
    internal class Surface : Figure, IRenderable
    {


        int[] _indices;
        IndexBuffer _indexBuffer;
        Matrix4 MVP;


        public override void Init()
        {
            base.Init();
        }

        private void GenerateIndices(int index)
        {
            int offsetArrayIndex = 6 * index;
            int offset = 4 * index;

            // 3, 2, 0, 0, 2, 1        7, 6, 4, 4, 6, 5
            // Triangle 1
            _indices[offsetArrayIndex] = offset + 3;
            _indices[offsetArrayIndex + 1] = offset + 2;
            _indices[offsetArrayIndex + 2] = offset + 0;

            // Triangle 2
            _indices[offsetArrayIndex + 3] = offset + 0;
            _indices[offsetArrayIndex + 4] = offset + 2;
            _indices[offsetArrayIndex + 5] = offset + 1;
        }
        public void Draw(Camera camera)
        {
            throw new NotImplementedException();
        }
    }
}
