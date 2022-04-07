using CompGraphEngine.SceneF;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CompGraphEngine
{
    internal class Window : GameWindow
    {
        public float DeltaTime;
        private float LastFrame;
        Scene scene;
        public static Window window { get; private set; }  
      


        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            window = this;
            scene = new TestScene();
        }

        protected override void OnLoad()
        {

           
            base.OnLoad();
            
            GL.ClearColor(0f, 0f, 0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            scene.Init();
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear( ClearBufferMask.DepthBufferBit|ClearBufferMask.ColorBufferBit);

            
            this.Title =this.MousePosition.ToString();
            scene.Render();
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            float curTime = (float)window.UpdateTime;
            DeltaTime = curTime  - LastFrame;
            LastFrame = curTime;

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            scene.Update();
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
