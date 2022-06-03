using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;

namespace CompGraphEngine.SceneF
{
    internal class SurfaceScene : Scene
    {
        public SurfaceScene(Window window) : base(window)
        {
            Renderer = new Render.Renderer3D();
            Camera = new Camera();
            Renderer.Camera = Camera;
        }

        BSurface surface;
        List<Circle> controlPolygon;


        float x, y, t;
        public override void Init()
        {


            Camera.Position = new Vector3(20f, 10, 10);
            Camera.Speed = 10f;

            controlPolygon = new List<Circle>();

            //for (int i = 0; i < 9; i++)
            //{
            //    Vector3 center = new Vector3();
            //    center.X = 0;
            //    center.Y = (float)MathHelper.Cos( i * MathHelper.Pi / 4);
            //    center.Z = (float)MathHelper.Sin( i * MathHelper.Pi / 4);
            //    Circle c = new Circle(center);

            //    controlPolygon.Add(c);
            //}




            surface = new BSurface(3, 3, 10, InputCoord());


            foreach (var l in surface.ControlPoints)
            {
                foreach (var c in l)
                {
                    c.Transform.Scale = new Vector3(0.01f);
                    c.color = Color4.Red;

                    AddObjectToScene(c);
                }
            }

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



            surface.Transform.RotateWithShift(new Vector3(0, 0, 5), new Vector3(0,0 , t));

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
                List < Circle > circles = new List<Circle>();
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

    }
}
