using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CompGraphEngine.SceneF
{
    internal class DepthTestSurface : Scene
    {
        BSurface surface;
        float x, y;
        public DepthTestSurface(Window window) : base(window)
        {
            Renderer = new Render.Renderer3D();
            Camera = new Camera();
            Renderer.Camera = Camera;
        }
        public override void Init()
        {

            GL.Enable(EnableCap.DepthTest);
           // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            Camera.Position = new Vector3(20f, 10, 10);
            Camera.Speed = 10f;

            surface = new BSurface(3, 3, 3, 10,10);
            surface.sh = new Render.OpenGLAPI.Shader("Shaders/surfaceWithDepth.glsl");

            AddObjectToScene(surface);
           
            AddObjectToScene(new Line(new Vector3(0), new Vector3(10, 0, 0), Color4.Red));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 10, 0), Color4.Yellow));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 0, 10), Color4.Blue));

            base.Init();
        }

        public override void Update()
        {
            x = window.MouseState.X;
            y = window.MouseState.Y;

            Camera.Yaw = 90 + x / 10f;
            Camera.Pitch = (-1) * y / 10f;

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
    }
}
