using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure2D;
using OpenTK.Mathematics;

namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {
        Line line;
        
        public override void Init()
        {
            line = new Line(
                 new Vector3(0f, 0f, 0),
                 new Vector3(0.1f, 0.5f, 0));

            AddObjectToScene(line);

            base.Init();
        }
        public override void Update()
        {
            float x =  2* Window.window.MousePosition.X/ Window.window.Size.X - 1f;
            float y = (-1) * (2*(Window.window.MousePosition.Y) / Window.window.Size.Y - 1f);
            line.PointToMouse(new Vector3( x, y, 0));
            
            base.Update();
        }

        
    }
}
