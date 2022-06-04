using CompGraphEngine.SceneF;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CompGraphEngine
{
    public class Window : GameWindow
    {
      
        Scene ploigons4lab, surface;
       
      
        public Window GetWindow()
        {
            return this;
        }

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            //surface = new SurfaceScene(this);
            ploigons4lab = new TestScene(this);


        }

        protected override void OnLoad()
        {

           
            base.OnLoad();
            
            GL.ClearColor(0f, 0f, 0f, 1.0f);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);

            //GL.Enable(EnableCap.DepthTest);
          

           //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
           ploigons4lab.Init();
           //surface.Init();

        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.DepthBufferBit|ClearBufferMask.ColorBufferBit);


            //surface.Render();
            ploigons4lab.Render();

            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            //this.Title = (1 / UpdateTime).ToString();
            this.Title = MouseState.Position.ToString();

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            

           //surface.Update();
           ploigons4lab.Update();

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
