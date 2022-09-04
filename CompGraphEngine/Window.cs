using CompGraphEngine.Engine;
using CompGraphEngine.Render.Model;
using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;
using Texture = CompGraphEngine.Render.Model.Texture;
using TextureBuffer = CompGraphEngine.Render.OpenGLAPI.Texture;

namespace CompGraphEngine
{
    public class Window : GameWindow
    {
        
       

        Model m;
        Shader shader;
        Camera Camera;
       
        Transform transform = new Transform();
        Transform lightPos = new Transform();



        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

            
           
            m =  ImportObj.ImportModel(@"C:\Users\Ilya\Desktop\3dmodels\blades\blades.obj");
            m.Display();
           
           

            shader = new Shader(@"./Shaders/shader.glsl");
            Camera = new Camera();
            Camera.Position = new Vector3(5, 5, 5);
            Camera.Speed = 20f;
            
            //transform.Rotate(x = -90f);
            //lightPos.Translate(3, 0, 0);
        }

        protected override void OnLoad()
        {

           
            base.OnLoad();
            
            GL.ClearColor(0f, 0f, 0f, 1.0f);



        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.DepthBufferBit|ClearBufferMask.ColorBufferBit);
           //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.Enable(EnableCap.DepthTest);


            shader.Use();

            Vector3 lightColor = new Vector3();
            lightColor.X = 1;
            lightColor.Y = 1;
            lightColor.Z = 1;

            Vector3 diffuseColor = lightColor * 0.5f;
            Vector3 ambientColor = diffuseColor * 0.2f;

            shader.SetVector3("light.position", lightPos.GetWorldPos().Xyz);
            shader.SetVector3("light.ambient", ambientColor);
            shader.SetVector3("light.diffuse", diffuseColor);
            shader.SetVector3("light.specular", new Vector3(1.0f));

          

            shader.SetMatrix4("projection", Camera.GetProjection3D());
            shader.SetMatrix4("view", Camera.GetViewMatrix());
            shader.SetMatrix4("model", transform.Model);
            shader.SetVector3("viewPos", Camera.Position);

           
            m.Draw(shader);
            
            SwapBuffers();
        }

        float x, y;
        float z = 0;
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
           this.Title = (1 / UpdateTime).ToString();
            
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }




            x = MouseState.X;
            y = MouseState.Y;

            Camera.Yaw = 90 + x / 5f;
           Camera.Pitch = (-1) * y / 5f;

            float tr = (float) (MathHelper.Sin(z));

            transform.Position = new Vector3(0, 0, 0);

            //lightPos.RotateWithShift(new Vector3(2, 3, 0), new Vector3(0, z, 0));



            moveCam();
            z+= 0.01f;
            base.OnUpdateFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            base.OnUnload();
        }


        void moveCam()
        {
            var state = KeyboardState;
            if (state.IsKeyDown(Keys.W))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.FORWARD, (float)UpdateTime);

            }
            if (state.IsKeyDown(Keys.A))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.LEFT, (float)UpdateTime);

            }
            if (state.IsKeyDown(Keys.S))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.BACKWARD, (float)UpdateTime);

            }
            if (state.IsKeyDown(Keys.D))
            {
                Camera.ProcessKeyboard(Camera.CameraMovement.RIGHT, (float)UpdateTime);

            }

        }
    }
}
