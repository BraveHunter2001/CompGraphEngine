using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;

namespace CompGraphEngine.SceneF
{
    internal class SurfaceScene : Scene
    {
        public SurfaceScene(Window window) : base(window)
        {
        }

        BSurface surface;
        BSurface surface1;

        float x, y, t;
        public override void Init()
        {
            
            Camera = new Camera();
            Camera.Position = new Vector3(20f, 10, 10);
            Camera.Speed = 10f;

            List<Circle> controlPolygon = new List<Circle>();

            for (int i = 0; i < 9; i++)
            {
                Vector3 center = new Vector3();
                center.X = 0;
                center.Y = (float)MathHelper.Cos(i * MathHelper.Pi / 4);
                center.Z = (float)MathHelper.Sin(i * MathHelper.Pi / 4);
                Circle c = new Circle(center);

                controlPolygon.Add(c);
            }


            surface = new BSurface(3,3, 10, BSurface.GeneratedPolygon(5,10));
           

            foreach (var l in surface.ControlPoints)
            {
                foreach (var c in l)
                {
                    //c.Transform.Scale = new Vector3(0.01f);
                    c.color = Color4.Red;

                    AddObjectToScene(c);
                }
            }

            AddObjectToScene(surface);
            AddObjectToScene(surface1);


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

           

            //surface.Transform.RotateWithShift( new Vector3(0,0,5), new Vector3(0, t, 0));

            t += 1f;
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
