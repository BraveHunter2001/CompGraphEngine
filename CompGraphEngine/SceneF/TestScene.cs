using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;

namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {
        Line line;
        Circle circle;
        
        public override void Init()
        {
            line = new Line(
                 new Vector3(0f, 0f, 0),
                 new Vector3(0f, 400f, 0), Color4.Aquamarine);

            circle = new Circle(new Vector3(0f, 0f, 0f),1f);
            AddObjectToScene(line);
           AddObjectToScene(circle);
            base.Init();
        }
        public override void Update()
        {
            //float x =  (Window.window.MousePosition.X - Window.window.Size.X/2);
           // float y = (-1) * (Window.window.MousePosition.Y -  Window.window.Size.Y/2);

            //line.Point2 = new Vector3(x,y, 0);
           
            
            base.Update();
        }

        
    }
}
