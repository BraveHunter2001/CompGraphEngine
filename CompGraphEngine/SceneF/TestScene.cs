using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure2D;
using OpenTK.Mathematics;

namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {
        public Line line;
        float x = 0.01f, y = 0.01f;
        public override void Init()
        {
            line = new Line(
                 new Vector3(0f, 0f, 0),
                 new Vector3(0.1f, 0f, 0));
            
            
            AddObjectToScene(line);
            base.Init();
        }
        public override void Update()
        {

            base.Update();
        }

        
    }
}
