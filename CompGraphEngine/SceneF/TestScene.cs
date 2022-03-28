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
                 new Vector3(0.1f, 0.5f, 0), Color4.Aquamarine);

            circle = new Circle(new Vector3(0f, 0f, 0f), 0.5f);
            AddObjectToScene(line);
            AddObjectToScene(circle);
            base.Init();
        }
        public override void Update()
        {
            float x =  2* Window.window.MousePosition.X/ Window.window.Size.X - 1f;
            float y = (-1) * (2*(Window.window.MousePosition.Y) / Window.window.Size.Y - 1f);

            line.Point2 = new Vector3(x,y, 0);
           
            
            base.Update();
        }

        
    }
}
