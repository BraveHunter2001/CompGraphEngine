using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;

namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {
        
        Circle circle;
        float x = 0;
        float y = 0;
        float scY = 0;
        public override void Init()
        {
           

            circle = new Circle(new Vector3(0f, 0f, 0f),1f);
            circle.Transform.Scale = new Vector3(40f, 40f, 1f);
           circle.Transform.Translate(0,0,0);
           
           
           AddObjectToScene(circle);
            base.Init();
        }
        public override void Update()
        {
            
            x = (-1)*(Constants.Width/2 - Window.window.MousePosition.X);
            y = Constants.Height/2 - Window.window.MousePosition.Y;
           
            scY = Window.window.MouseState.Scroll.Y;
            System.Console.WriteLine($"X: {x} Y: {y}| DeltaScrolls  Y: {scY}");
           
            circle.Transform.Position = new Vector3(x,y,0);
            circle.Transform.Scale = new Vector3(scY,scY,0);
            base.Update();
        }

        
    }
}
