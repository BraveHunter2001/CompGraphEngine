using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;

namespace CompGraphEngine.SceneF
{
    internal class TestScene : Scene
    {


        Surface surface;
        Circle circle;
        BSpline bSpline;
        Random random;
        float x = 0, y = 0, t = 0;
        bool isPrintCircle = false;

        List<Vector3> pointsForPoligon;
        List<Circle> pointsC;

        public TestScene(Window window) : base(window)
        {
            Renderer = new Render.Renderer2D();
            Camera = new Camera();
            Renderer.Camera = Camera;
            window.MouseDown += PressedMouse;
            window.KeyDown += PressedKey;
            random = new Random();
            pointsForPoligon = new List<Vector3>();
            pointsC = new List<Circle>();
        }

        public override void Init()
        {
            Console.WriteLine("Spacebar - switching between point input and polygon rendering. ");
            Console.WriteLine("Press spacebar...");
            
            Camera.Position = new Vector3(0, 0, 1);
            Camera.Speed = 10f;

            //AddObjectToScene(circle);
            //AddObjectToScene(new Line(new Vector3(0), new Vector3(10, 0, 0), Color4.Red));
            //AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 10, 0), Color4.Yellow));
            //AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 0, 10), Color4.Blue));
            
           
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


        
        void PressedMouse(MouseButtonEventArgs arg)
        {

            if (arg.Action == InputAction.Press && arg.Button == MouseButton.Left)
            {
                
                if (isPrintCircle)
                {

                    float x = window.MouseState.X - Constants.Width / 2;
                    float y = (-1) * (window.MouseState.Y - Constants.Height / 2);
                    pointsForPoligon.Add(new Vector3(x, y, 0));

                    DrawHelperPointsAndLine(x, y);

                 
                }
            }
        }

        void DrawHelperPointsAndLine(float x, float y )
        {
            var c = new Circle(new Vector3(x, y, 0));

            float r = random.Next(0, 255) * 1f / 255;
            float g = random.Next(0, 255) * 1f / 255;
            float b = random.Next(0, 255) * 1f / 255;
            c.color = new Color4(r, g, b, 0.5f);
            
            c.Transform.Scale = new Vector3(10, 10, 10);
            pointsC.Add(c);
            AddObjectToScene(c);
        }
        void PressedKey(KeyboardKeyEventArgs arg)
        {
            if (arg.Key == Keys.Space)
            {
                isPrintCircle = !isPrintCircle;
                if (isPrintCircle)
                {
                    System.Console.WriteLine("You can input points");
                    if(pointsForPoligon.Count !=0)
                    {
                        foreach(var c in pointsC)
                        {
                            RemoveObjectFromScene(c);
                        }
                        pointsC.Clear();
                        pointsForPoligon.Clear();
                    }
                }
                else
                {
                    System.Console.WriteLine("You cant input points");
                    float r = random.Next(0, 255) * 1f / 255;
                    float g = random.Next(0, 255) * 1f / 255;
                    float b = random.Next(0, 255) * 1f / 255;
                    Color4 color = new Color4(r, g, b, 1f);
                    Poligon poligon = new Poligon(pointsForPoligon, color);
                    AddObjectToScene(poligon);
                }
                    
            }
        }
       

        void InputControlPoint()
        {

        }
    }
}
