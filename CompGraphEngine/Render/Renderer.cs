using CompGraphEngine.Engine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CompGraphEngine.Render
{
    public class Renderer
    {
        internal void Draw(ref VertexArray va, ref VertexBuffer vb, ref Shader shader)
        {
            shader.Use();
            va.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, vb.CountVertex);
        }

        internal void Draw(ref VertexArray va, ref VertexBuffer vb, ref  IndexBuffer ib, ref Shader shader)
        {
            shader.Use();
            va.Bind();
            ib.Bind();

            GL.DrawElements(PrimitiveType.Triangles, ib.GetCount(),DrawElementsType.UnsignedInt, 0);
        }

        public void Draw(IRenderable renderable, Camera camera)
        {
            renderable.Draw(camera);
        }

        
        public Vector2 GetWindowPosObj(GameObject obj, Camera cam)
        {
            Vector4 pos = new Vector4(obj.Transform.Position);
            pos.W = 1;
            Vector4 clipSpacePos = cam.GetProjection3D() * cam.GetViewMatrix() * pos;
            Vector3 ndcSpacePos = clipSpacePos.Xyz / clipSpacePos.W;
            Vector2 viewSize = new Vector2(Constants.Width, Constants.Height);


            Vector2 windowSpacePos = new Vector2((float)((ndcSpacePos.X + 1.0) / 2f) * viewSize.X, (float)((1.0 - ndcSpacePos.Y) / 2.0f) * viewSize.Y);

            return windowSpacePos;
        }
     
    }
}
