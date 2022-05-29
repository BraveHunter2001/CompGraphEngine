using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace CompGraphEngine.Render
{
    public class RenderObjectsElements: RenderObject
    {
        IndexBuffer _indexBuffer;
        int[] _indexes;
        public RenderObjectsElements(float[,] Points, float[,] Colors, Shader shader, Matrix4 model, int[] Indexes)
        {
            _vertPoints = Points;
            _vertColors = Colors;
            _indexes = Indexes;
            _shader = shader;
            Model = model;
            _indexBuffer = new IndexBuffer(_indexes, _indexes.Length);

        }
        public override void Draw()
        {
            _vertexArray.Bind();
            _indexBuffer.Bind();

            GL.DrawElements(PrimitiveType.Triangles, _indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
        }

    }
}
