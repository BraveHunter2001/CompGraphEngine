using CompGraphEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CompGraphEngine
{
    internal class Window : GameWindow
    {

        Triangle2D triangle2D;
        Triangle2D triangle2D_2;
        private readonly float[] _vertices =
        {
             0.5f,  0.5f, 0.0f, 0.7f, 0.5f, 1.0f, 1.0f, // Bottom-left vertex
             0.5f,  -0.5f, 0.0f, 1.0f, 0.5f, 0.7f, 1.0f,// Bottom-right vertex
             -0.5f,  0.5f, 0.0f,  0.0f, 0.5f, 0.0f, 1.0f // Top vertex
        };

        private readonly float[] _vertices_2 =
        {
             0.4f, -0.6f, 0.0f, 1.0f, 0.5f, 0.0f, 1.0f, // Bottom-left vertex
             -0.6f, -0.6f, 0.0f, 1.0f, 0.5f, 0.0f, 1.0f,// Bottom-right vertex
             -0.6f,  0.4f, 0.0f,  1.0f, 0.5f, 0.0f, 1.0f // Top vertex
        };


        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            triangle2D = new Triangle2D(_vertices);
            triangle2D_2 = new Triangle2D(_vertices_2);

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0f, 0f, 0f, 1.0f);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);


            triangle2D.render();
            triangle2D_2.render();

            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (KeyboardState.IsKeyDown(Keys.Escape))
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

            // triangle2D.unload();
            // triangle2D_2.unload();

            base.OnUnload();
        }
    }
}
