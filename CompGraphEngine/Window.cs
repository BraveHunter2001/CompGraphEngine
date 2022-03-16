using CompGraphEngine.Render;
using CompGraphEngine.Render.Figure2D;
using CompGraphEngine.Util;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;

namespace CompGraphEngine
{
    internal class Window : GameWindow
    {
        private readonly float[] _vertices =
        {
             0.5f,  0.5f, 0.0f, 0.7f, 0.5f, 1.0f, 1.0f, // Bottom-left vertex
             0.5f,  -0.5f, 0.0f, 1.0f, 0.5f, 0.7f, 1.0f,// Bottom-right vertex
             -0.5f,  0.5f, 0.0f,  0.0f, 0.5f, 0.0f, 1.0f // Top vertex
        };

        Renderer _renderer;

        Triangle tr;
        Triangle tr2;
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Vector3f p1 = new Vector3f(0.0f, 0.5f, 0.0f);
            Vector3f p2 = new Vector3f(0.5f, 0f, 0.0f);
            Vector3f p3 = new Vector3f(-0.5f, 0.0f, 0.0f);

            tr = new Triangle(p1, p2, p3);
            tr2 = new Triangle(_vertices);

            _renderer = new Renderer();
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

            _renderer.Draw(tr2);
            _renderer.Draw(tr);
            
          

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

            base.OnUnload();
        }
    }
}
