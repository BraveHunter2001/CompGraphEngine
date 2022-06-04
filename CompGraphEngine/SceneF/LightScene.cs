using CompGraphEngine.Engine;
using CompGraphEngine.Engine.Figure;
using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.SceneF
{
    internal class LightScene : Scene
    {
        float x, y;
        
        Cube cube;
        Cube cube1;
        Cube lightcube;
        Cube graund;
        Vector3 lightPos;
        public LightScene(Window window) : base(window)
        {
            Renderer = new Render.Renderer3D();
            Camera = new Camera();
            Renderer.Camera = Camera;
            
        }

        public override void Init()
        {

            GL.Enable(EnableCap.DepthTest);
           
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            Camera.Position = new Vector3(20f, 10, 0);
            Camera.Speed = 50f;

            
            cube = new Cube();
            cube.color = Color4.Coral;

            graund = new Cube();
            graund.color = Color4.SandyBrown;
            graund.Transform.Translate(0, -110, 0);
            graund.Transform.Scale = new Vector3(100);
            

            cube1 = new Cube();
            cube1.color = Color4.Purple;
            cube1.Transform.Position = new Vector3(0, 1, -10);

            lightPos = new Vector3(5, 0, -5);

            lightcube = new Cube();
            lightcube.color = Color4.White;
            lightcube.sh = new Shader("Shaders/light_cube.glsl");
            lightcube.Transform = new Transform(lightPos, new Vector3(1,1,1), new Vector3(0,0,0));
            
            lightcube.Transform.Scale = new Vector3(0.5f);

            
            AddObjectToScene(cube);
            AddObjectToScene(cube1);
            AddObjectToScene(lightcube);
           // AddObjectToScene(graund);

            AddObjectToScene(new Line(new Vector3(0), new Vector3(10, 0, 0), Color4.Red));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 10, 0), Color4.Yellow));
            AddObjectToScene(new Line(new Vector3(0), new Vector3(0, 0, 10), Color4.Blue));

            base.Init();

           
        }
        float t = 0;
        public override void Update()
        {
            x = window.MouseState.X;
            y = window.MouseState.Y;

            Camera.Yaw = 90 + x / 10f;
            Camera.Pitch = (-1) * y / 10f;

            lightcube.Transform.RotateWithShift(new Vector3(0,0,5), new Vector3(0,t,0));
            //cube.Transform.Position = new Vector3((float)MathF.Sin(t) * 5, 0, 0);

            cube.sh.SetVector3("lightPos", lightcube.Transform.GetWorldPos().Xyz);
            cube.sh.SetVector3("viewPos", Camera.Position);
            Vector4 color = (Vector4)lightcube.color;
            cube.sh.SetVector3("lightColor", color.Xyz);


            cube1.sh.SetVector3("lightPos", lightcube.Transform.GetWorldPos().Xyz);
            cube1.sh.SetVector3("viewPos", Camera.Position);
            cube1.sh.SetVector3("lightColor", color.Xyz);

            graund.sh.SetVector3("lightPos", lightcube.Transform.GetWorldPos().Xyz);
            graund.sh.SetVector3("viewPos", Camera.Position);
            graund.sh.SetVector3("lightColor", color.Xyz);

            t += 0.5f;
            moveCam();
            base.Update();
        }

        public override void Render()
        {
            
            base.Render();

           
           
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
