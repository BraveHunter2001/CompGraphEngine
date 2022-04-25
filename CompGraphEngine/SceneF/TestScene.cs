using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
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
            window.MouseDown += PressedMouse;
            window.MouseUp += Realised;
        }

        public override void Init()
        {

            Camera = new Camera();
            Camera.Position = new Vector3(0f, 0, 0);
            Camera.Speed = 10f;

            AddObjectToScene(new Line(new Vector3(0), new Vector3(10, 0, 0), Color4.Red));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 10, 0), Color4.Yellow));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 0, 10), Color4.Blue));

            circle = new Circle();
            circle.Transform.Position = new Vector3(0, 0, 0);
            AddObjectToScene(circle);
            System.Console.WriteLine(circle.Transform.Scale);
            base.Init();
        }
        public override void Update()
        {

            x = window.MouseState.X;
            y = window.MouseState.Y;

            //Camera.Yaw =  -90 + x / 10f;
            //Camera.Pitch = (-1) * y / 10f;


            //System.Console.WriteLine(Renderer.GetWindowPosObj(circle, Camera));
            //System.Console.WriteLine(window.MouseState.Position);

            
            //t += 0.001f;
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

        void PressedMouse(MouseButtonEventArgs arg)
        {
              
            if (arg.Action == InputAction.Press && arg.Button == MouseButton.Left)
            {
                 System.Console.WriteLine("Pressed");
              
                Vector2 m= window.MouseState.Position;
                System.Console.WriteLine($"Mouse: {m}");

                if (circle.isContain(m.X, m.Y, Camera))
                    System.Console.WriteLine("Popal");
                else
                    System.Console.WriteLine("Nepopal");
            }
        }

        void Realised(MouseButtonEventArgs arg)
        {
            if (arg.Action == InputAction.Release && arg.Button == MouseButton.Left)
            {
                // System.Console.WriteLine("Relize");
            }
        }
    }
}
