using CompGraphEngine.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CompGraphEngine
{
    internal class Window : GameWindow
    {
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if ( KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close(); 
            }
            base.OnUpdateFrame(args);
        }
    }
}
