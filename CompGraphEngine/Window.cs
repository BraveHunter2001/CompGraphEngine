using CompGraphEngine.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;

namespace CompGraphEngine
{
    internal class Window : GameWindow
    {
        //private readonly float[] _vertices =
        // {
        //     0.5f,  0.5f, 0.0f, // top right
        //     0.5f, -0.5f, 0.0f, // bottom right
        //    -0.5f, -0.5f, 0.0f, // bottom left
        //    -0.5f,  0.5f, 0.0f, // top left
        //};

        //// Then, we create a new array: indices.
        //// This array controls how the EBO will use those vertices to create triangles
        //private readonly uint[] _indices =
        //{
        //    // Note that indices start at 0!
        //    0, 1, 3, // The first triangle will be the bottom-right half of the triangle
        //    1, 2, 3  // Then the second will be the top-right half of the triangle
        //};

        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
        };
        private Stopwatch _timer;
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private Shader _shader;
        private int _elementBufferObject;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0f, 0f, 0f, 1.0f);
            
            //VBO 
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            
            // VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);


            ////EBO
            //_elementBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.GetInteger(GetPName.MaxVertexAttribs, out int maxAttributeCount);
            System.Console.WriteLine($"Maximum number of vertex attributes supported: {maxAttributeCount}");

            _shader = new Shader("Shaders/shader.glsl");
            _shader.Use();

            _timer = new Stopwatch();
            _timer.Start();
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            _shader.Use();


            double timeValue = _timer.Elapsed.TotalSeconds;
            float greenValue = (float)System.Math.Sin(timeValue) / 2.0f + 0.5f;
            int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            
            GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

            GL.BindVertexArray(_vertexArrayObject);

            // GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if ( KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close(); 
            }
            base.OnUpdateFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(_shader.Handle);

            base.OnUnload();
        }
    }
}
