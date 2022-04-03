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
            

           circle.Transform.Position = new Vector3((float)MathHelper.Cos(x), (float)MathHelper.Sin(x), 0);
            circle.Thickness = (float)MathHelper.Abs(MathHelper.Sin(x));
            x += 0.01f;
            base.Update();
        }

        
    }
}
