using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;

namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {
        
        Circle circle;
        float radius;
        float x = 0, y = 0; 
        public override void Init()
        {
            //Gismo
            //AddObjectToScene(new Line(new Vector3(0, 0, 0), new Vector3(1000, 0, 0), Color4.Red));
            //AddObjectToScene(new Line(new Vector3(0, 0, 0), new Vector3(0, 1000, 0), Color4.Green));
            //AddObjectToScene(new Line(new Vector3(0, 0, 0), new Vector3(0, 0, 1000), Color4.Blue));

            Camera = new Camera();
            Camera.Position = new Vector3(2.5f, 0, 0);

            circle = new Circle(new Vector3(0f, 0f, 0f),1f);
            circle.Transform.Position = new Vector3(0f , 0f, 0f);
            circle.Transform.Scale = new Vector3(5f,5f,5f);
            circle.Transform.Rotation = new Vector3(0f, 50 ,0);
           
           
            AddObjectToScene(circle);
            AddObjectToScene(new Circle(new Vector3(0, 0, 0),Color4.Yellow));
            
            base.Init();
        }
        public override void Update()
        {

            radius = Window.window.MouseState.Scroll.Y;
            
            System.Console.WriteLine(radius);
            Camera.Pitch = radius ;
            
         
            
          

            base.Update();
        }

        
    }
}
