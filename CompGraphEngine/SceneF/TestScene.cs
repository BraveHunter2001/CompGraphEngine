using CompGraphEngine.Render.Figure2D;
using CompGraphEngine.Util;

namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {
        Line line;
        public override void Init()
        {
            line = new Line(
                 new Vector3f(0f, 0f, 0),
                 new Vector3f(100f, 300f, 0));
            
            
            AddObjectToScene(line);
            base.Init();
        }

        public override void Update()
        {
           // line.TestCoordsRedraws(300f, 300f);
            base.Update();
        }
    }
}
