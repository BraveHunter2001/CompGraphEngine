using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
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
            Camera.Position = new Vector3(0f, 0, 50);
            Camera.Speed = 1000f;

            circle = new Circle(new Vector3(0f, 0f, 0f), 1f);
            circle.Transform.Position = new Vector3(0f, 0f, 0f);
            circle.Transform.Scale = new Vector3(5f, 5f, 5f);
            circle.Transform.Rotation = new Vector3(0f, 50, 0);


            AddObjectToScene(circle);
            AddObjectToScene(new Circle(new Vector3(0, 0, 0), Color4.Yellow));

            base.Init();
        }
        public override void Update()
        {

            radius = Window.window.MouseState.Scroll.Y;

            System.Console.WriteLine($"{Camera.Position} {Camera.Yaw}");
            Camera.Yaw = radius;




            moveCam();
            base.Update();
        }

        void moveCam()
        {
            var state = Window.window.KeyboardState;
            if (state.IsKeyPressed(Keys.W))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.FORWARD, (float)Window.window.UpdateTime);
                System.Console.WriteLine("Pressed w");
            }
            if (state.IsKeyPressed(Keys.A))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.LEFT, (float)Window.window.UpdateTime);
                System.Console.WriteLine("Pressed A");
            }
            if (state.IsKeyPressed(Keys.S))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.BACKWARD, (float)Window.window.UpdateTime);
                System.Console.WriteLine("Pressed S");
            }
            if (state.IsKeyPressed(Keys.D))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.RIGHT, (float)Window.window.UpdateTime);
                System.Console.WriteLine("Pressed D");
            }

        }
    }
}
