using CompGraphEngine.Engine;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;


namespace CompGraphEngine
{
    
    internal class Program
    {
        
        static void Main(string[] args)
        {


            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(Constants.Width, Constants.Height),
                Title = "LearnOpenTK - Creating a Window",
                
                Flags = ContextFlags.ForwardCompatible,
            };

            // To create a new window, create a class that extends GameWindow, then call Run() on it.
            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                
                window.Run();
            }
        }
    }
}
