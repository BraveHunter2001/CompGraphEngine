using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using CompGraphEngine.Engine.Menu;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;

namespace CompGraphEngine.SceneF
{
    internal class DepthTestSurface : Scene
    {
        BSurface surface;
        List<Circle> controlPolygon;
        Menu menu;
        float x, y;
        public DepthTestSurface(Window window) : base(window)
        {
            Renderer = new Render.Renderer3D();
            Camera = new Camera();
            Renderer.Camera = Camera;

            MenuItem randSurface = new MenuItem(RandomSurface, "Random surface");
            MenuItem inputFullCoord = new MenuItem(InputFullCoord, "Input full coord");
            MenuItem InputPipeBYX = new MenuItem(InputCirclePolygonByX, "Input pipe-surface by x");
            MenuItem InputPipeBYY = new MenuItem(InputCirclePolygonByY, "Input pipe-surface by y");
            MenuItem InputPipeBYZ = new MenuItem(InputCirclePolygonByZ, "Input pipe-surface by z");

            MenuItem[] menuItems = { randSurface, inputFullCoord, InputPipeBYX, InputPipeBYY, InputPipeBYZ };
            menu = new Menu("Create surface", menuItems);
        }
        public override void Init()
        {

            GL.Enable(EnableCap.DepthTest);
           //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            Camera.Position = new Vector3(20f, 10, 0);
            Camera.Speed = 50f;

            menu.Select();



            AddObjectToScene(surface);
            
            AddObjectToScene(new Line(new Vector3(0), new Vector3(10, 0, 0), Color4.Red));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 10, 0), Color4.Yellow));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 0, 10), Color4.Blue));

            base.Init();
        }
        float t  = 0;
        public override void Update()
        {
            x = window.MouseState.X;
            y = window.MouseState.Y;

            surface.Transform.RotateWithShift(new Vector3(5,5,5), new Vector3(t,t,0));

            Camera.Yaw = 90 + x / 10f;
            Camera.Pitch = (-1) * y / 10f;

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

        List<List<Circle>> InputCoord()
        {
            List<List<Circle>> result = new List<List<Circle>>();
            Console.WriteLine("Input control points betwen space.");

            Console.WriteLine("Input count point by T - param");
            int t = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input count point by U - param");
            int u = Int32.Parse(Console.ReadLine());
            int countPoint = 0;
            for (int i = 0; i < t; i++)
            {
                List<Circle> circles = new List<Circle>();
                for (int j = 0; j < u; j++)
                {
                    Console.WriteLine($"Point {countPoint} X Y Z:");
                    string[] strXYZ = Console.ReadLine().Split(' ');

                    Vector3 center = new Vector3();
                    center.X = Int32.Parse(strXYZ[0]);
                    center.Y = Int32.Parse(strXYZ[1]);
                    center.Z = Int32.Parse(strXYZ[2]);
                    Circle c = new Circle(center);

                    circles.Add(c);
                    countPoint++;
                }
                result.Add(circles);
            }
            return result;
        }


        void RandomSurface()
        {
            surface = new BSurface(3, 3, 10, 10, 10);
        }

        void InputFullCoord()
        {
            surface = new BSurface(2, 2, 10, InputCoord());
        }

        void InputCirclePolygonByX()
        {
            controlPolygon = new List<Circle>();

            for (int i = 0; i < 9; i++)
            {
                Vector3 center = new Vector3();
                center.X = 0;
                center.Y = (float)MathHelper.Cos(i * MathHelper.Pi / 4);
                center.Z = (float)MathHelper.Sin(i * MathHelper.Pi / 4);
                Circle c = new Circle(center);

                controlPolygon.Add(c);
            }
            surface = new BSurface(3, 3, 10, BSurface.GeneratedPolygonByX(controlPolygon, 5, 5));
        }
        void InputCirclePolygonByY()
        {
            controlPolygon = new List<Circle>();

            for (int i = 0; i < 9; i++)
            {
                Vector3 center = new Vector3();
                center.X = (float)MathHelper.Cos(i * MathHelper.Pi / 4);
                center.Y = 0;
                center.Z = (float)MathHelper.Sin(i * MathHelper.Pi / 4);
                Circle c = new Circle(center);

                controlPolygon.Add(c);
            }
            surface = new BSurface(3, 3, 10, BSurface.GeneratedPolygonByY(controlPolygon, 5, 5));
        }
        void InputCirclePolygonByZ()
        {
            controlPolygon = new List<Circle>();

            for (int i = 0; i < 9; i++)
            {
                Vector3 center = new Vector3();
                center.X = (float)MathHelper.Cos(i * MathHelper.Pi / 4);
                center.Y = (float)MathHelper.Sin(i * MathHelper.Pi / 4);
                center.Z = 0;
                Circle c = new Circle(center);

                controlPolygon.Add(c);
            }
            surface = new BSurface(3, 3, 10, BSurface.GeneratedPolygonByZ(controlPolygon, 5, 5));
        }
    }
}
