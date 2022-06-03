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
            Renderer = new Render.Renderer2D();
            Camera = new Camera();
            Renderer.Camera = Camera;
           // window.
        }

        public override void Init()
        {

            circle = new Circle(new Vector3(0,0,0));
            circle.Transform.Scale = new Vector3(10,10,10);
            Camera.Position = new Vector3(0, 0, 1);
            Camera.Speed = 10f;

            AddObjectToScene(circle);
            AddObjectToScene(new Line(new Vector3(0), new Vector3(10, 0, 0), Color4.Red));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 10, 0), Color4.Yellow));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 0, 10), Color4.Blue));
            
           
            base.Init();
        }
        public override void Update()
        {
            x = window.MouseState.X;
            y = window.MouseState.Y;

            //Camera.Yaw = 90 + x / 10f;
           // Camera.Pitch = (-1) * y / 10f;



            
            moveCam();
            base.Update();
        }

        void moveCam()
        {
            var state = window.KeyboardState;
            if (state.IsKeyDown(Keys.W))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.FORWARD, (float)window.UpdateTime);
                
            }
            if (state.IsKeyDown(Keys.A))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.LEFT, (float)window.UpdateTime);
                
            }
            if (state.IsKeyDown(Keys.S))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.BACKWARD, (float)window.UpdateTime);
                
            }
            if (state.IsKeyDown(Keys.D))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.RIGHT, (float)window.UpdateTime);
               
            }

        }


        Vector2 lastMouse = new Vector2();
        void PressedMouse(MouseButtonEventArgs arg)
        {

            if (arg.Action == InputAction.Press && arg.Button == MouseButton.Left)
            {
                System.Console.WriteLine("Pressed"); 
                Vector2 m = window.MouseState.Position;
                if (circle.isContain(m.X, m.Y, Camera))
                {
                    lastMouse = m;
                    System.Console.WriteLine("Popal");
                }
            }
        }

        void Realised(MouseButtonEventArgs arg)
        {
            if (arg.Action == InputAction.Release && arg.Button == MouseButton.Left)
            {
                System.Console.WriteLine("Relize");
                Vector2 m = window.MouseState.Position;
                Vector3 def = new Vector3(m - lastMouse) / 100f;
                def.Y *= -1;
                circle.Transform.Translate(def);
            }
        }

        void InputControlPoint()
        {

        }
    }
}
