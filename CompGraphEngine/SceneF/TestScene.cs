using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;

namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {
        Line line;
        Circle circle;
        float x = 0;
        float y = 0;
        public override void Init()
        {
            line = new Line(
                 new Vector3(0f, 0f, 0),
                 new Vector3(0f, 400f, 0), Color4.Aquamarine);

            circle = new Circle(new Vector3(0f, 0f, 0f),1f);
            circle.Transform.Scale = new Vector3(40f, 40f, 1f);
           circle.Transform.Translate(0,0,0);
           
            AddObjectToScene(line);
           AddObjectToScene(circle);
            base.Init();
        }
        public override void Update()
        {
            

           circle.Transform.Position = new Vector3((float)MathHelper.Cos(x), (float)MathHelper.Sin(x), 0);

            x += 0.01f;
            base.Update();
        }

        
    }
}
