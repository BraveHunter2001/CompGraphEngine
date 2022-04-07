﻿using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {

        Circle circle;
        Surface surface;
        float radius;
        float t = 0;
        float x = 0, y = 0;
        public override void Init()
        {
        
            Camera = new Camera();
            Camera.Position = new Vector3(0f, 0, 50);
            Camera.Speed = 10f;

            surface = new Surface();
            surface.Transform.Scale = new Vector3(5, 5, 5);

            //circle = new Circle(new Vector3(0f, 0f, 0f), 1f);
            //circle.Transform.Position = new Vector3(0f, 0f, 0f);
            //circle.Transform.Scale = new Vector3(5f, 5f, 5f);
            //circle.Transform.Rotation = new Vector3(0f, 50, 0);


            //AddObjectToScene(circle);
            //AddObjectToScene(new Circle(new Vector3(0, 0, 1), Color4.Yellow));
            AddObjectToScene(surface);

            base.Init();
        }
        public override void Update()
        {

            radius = Window.window.MouseState.Scroll.Y;

            System.Console.WriteLine($"{Camera.Position} {Camera.Yaw}");
           Camera.Yaw = -90 + (-1) * radius;
           

            

            t += 0.01f;
            moveCam();
            base.Update();
        }

        void moveCam()
        {
            var state = Window.window.KeyboardState;
            if (state.IsKeyDown(Keys.W))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.FORWARD, (float)Window.window.UpdateTime);
                System.Console.WriteLine("Pressed W");
            }
            if (state.IsKeyDown(Keys.A))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.LEFT, (float)Window.window.UpdateTime);
                System.Console.WriteLine("Pressed A");
            }
            if (state.IsKeyDown(Keys.S))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.BACKWARD, (float)Window.window.UpdateTime);
                System.Console.WriteLine("Pressed S");
            }
            if (state.IsKeyDown(Keys.D))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.RIGHT, (float)Window.window.UpdateTime);
                System.Console.WriteLine("Pressed D");
            }

        }
    }
}
