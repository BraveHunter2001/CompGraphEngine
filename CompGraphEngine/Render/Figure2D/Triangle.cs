using OpenTK.Graphics.OpenGL4;

namespace CompGraphEngine.Render.Figure2D
{
    public class Triangle : Figure
    {
        const int _sizePosition = 3, _sizeColor = 4;

        
        public Triangle(float[] vert)
        {
            _vertices = vert;
            Init();
        }
        public override void Init()
        {
            base.Init();
            _shader = new Shader("Shaders/triangle.glsl");

            _layout.Push<float>(_sizePosition);
            _layout.Push<float>(_sizeColor);
            _vertexArray.AddBuffer(ref _vertexBuffer, ref _layout);
        }

        internal override void Draw()
        {
            _shader.Use();
            _vertexArray.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertexBuffer.CountVertex);
        }
    }
}
