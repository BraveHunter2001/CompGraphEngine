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

namespace CompGraphEngine
{
    public class Window : GameWindow
    {
        private List<int> GenerateIndices()
        {

            List<int> inds_l = new List<int>();
            int[] inds = new int[] {2, 3, 4,
            8, 7, 6,
            5, 6, 2,
            6, 7, 3,
            3, 7, 8,
            1, 4, 8,
            1, 2, 4,
            5, 8, 6,
            1, 5, 2,
            2, 6, 3,
            4, 3, 8,
            5, 1, 8};


            for (int i = 0; i < inds.Length; i++)
                inds[i] -= 1;

            for (int i = 0; i < inds.Length; i++)
                inds_l.Add(inds[i]);

            return inds_l;

        }

        List<Vertex> FillCoordsVertex()
        {
            List<Vertex> vertices = new List<Vertex>();
            
            Vertex v1 = new Vertex();
            v1.Position = new Vector3(1.0f, -1.0f, -1.0f);
            v1.TexCoords = new Vector2(0f);
            v1.Normal = new Vector3(0f);
            vertices.Add(v1);

            Vertex v2 = new Vertex();
            v2.Position = new Vector3(1.0f, -1.0f, 1.0f);
            v2.TexCoords = new Vector2(0f);
            v2.Normal = new Vector3(0f);
            vertices.Add(v2);

            Vertex v3 = new Vertex();
            v3.Position = new Vector3(-1.0f, -1.0f, 1.0f);
            v3.TexCoords = new Vector2(0f);
            v3.Normal = new Vector3(0f);
            vertices.Add(v3);


            Vertex v4 = new Vertex();
            v4.Position = new Vector3(-1.0f, -1.0f, -1.0f);
            v4.TexCoords = new Vector2(0f);
            v4.Normal = new Vector3(0f);
            vertices.Add(v4);

            

            Vertex v5 = new Vertex();
            v5.Position = new Vector3(1.0f, 1.0f, -1.0f);
            v5.TexCoords = new Vector2(0f);
            v5.Normal = new Vector3(0f);
            vertices.Add(v5);

            

            Vertex v6 = new Vertex();
            v6.Position = new Vector3(1.0f, 1.0f, 1.0f);
            v6.TexCoords = new Vector2(0f);
            v6.Normal = new Vector3(0f);
            vertices.Add(v6);

            

            Vertex v7 = new Vertex();
            v7.Position = new Vector3(-1.0f, 1.0f, 1.0f);
            v7.TexCoords = new Vector2(0f);
            v7.Normal = new Vector3(0f);
            vertices.Add(v7);

            

            Vertex v8 = new Vertex();
            v8.Position = new Vector3(-1.0f, 1.0f, -1.0f);
            v8.TexCoords = new Vector2(0f);
            v8.Normal = new Vector3(0f);
            vertices.Add(v8);


            return vertices;
        }

       

        Model m;
        Shader shader;
        Camera Camera;


        

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

            List<Texture> tex = new List<Texture>();
          

            List<Vertex> vertices = FillCoordsVertex();
            List<int> inds = GenerateIndices();


            Mesh mesh = ImportObj.ImportMesh(@"C:\Users\Ilya\Desktop\3MODEL\untitled.obj");

            //Title =  mesh.name;
            List<Mesh> meshes = new List<Mesh>();

            meshes.Add(mesh);

            m = new Model(meshes);
           

            shader = new Shader(@"./Shaders/shader.glsl");
            Camera = new Camera();
            Camera.Position = new Vector3(5, 5, 5);
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
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.Enable(EnableCap.DepthTest);


            shader.Use();
            shader.SetMatrix4("projection", Camera.GetProjection3D());
            shader.SetMatrix4("view", Camera.GetViewMatrix());
            shader.SetMatrix4("model", Matrix4.Identity);

            m.Draw(shader);
            
            SwapBuffers();
        }

        float x, y;
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
           // this.Title = (1 / UpdateTime).ToString();
            
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }




            x = MouseState.X;
            y = MouseState.Y;

            Camera.Yaw = 90 + x / 5f;
           Camera.Pitch = (-1) * y / 5f;








            moveCam();
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
