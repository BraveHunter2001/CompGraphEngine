using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {

       
        Surface surface;
        Circle circle;
        BSpline bSpline;

        float x = 0, y = 0, t = 0;

        public TestScene(Window window) : base(window)
        {
        }

        public override void Init()
        {
            
            Camera = new Camera();
            Camera.Position = new Vector3(0f, 0, 50);
            Camera.Speed = 10f;

            surface = new Surface();
            surface.Transform.Scale = new Vector3(2f);
            surface.Transform.Rotation = new Vector3(10, 0, 0);

            bSpline = new BSpline();
            bSpline.Transform.Scale = new Vector3(1);

            foreach( var cp in bSpline.ControlPoints)
            {
                cp.Transform.Scale = new Vector3(0.01f);
                AddObjectToScene(cp);
            }

            AddObjectToScene(bSpline);

            // circle = new Circle(new Transform());
            // circle.Transform.Scale = new Vector3(10);

             //AddObjectToScene(surface);
            AddObjectToScene(new Line(new Vector3(0), new Vector3(10, 0, 0), Color4.Red));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 10, 0), Color4.Yellow));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 0, 10), Color4.Blue));
            // AddObjectToScene(circle);

            base.Init();
        }
        public override void Update()
        {

            x = window.MouseState.X;
            y = window.MouseState.Y;

          Camera.Yaw =  -90 + x / 10f;
          Camera.Pitch = (-1) * y / 10f;

            
           // surface.updateTime(t % 360);
            t += 0.01f;
            moveCam();
            base.Update();
        }

        void moveCam()
        {
            var state = window.KeyboardState;
            if (state.IsKeyDown(Keys.W))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.FORWARD, (float)window.UpdateTime);
                System.Console.WriteLine("Pressed W");
            }
            if (state.IsKeyDown(Keys.A))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.LEFT, (float)window.UpdateTime);
                System.Console.WriteLine("Pressed A");
            }
            if (state.IsKeyDown(Keys.S))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.BACKWARD, (float)window.UpdateTime);
                System.Console.WriteLine("Pressed S");
            }
            if (state.IsKeyDown(Keys.D))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.RIGHT, (float)window.UpdateTime);
                System.Console.WriteLine("Pressed D");
            }

        }
    }
}
