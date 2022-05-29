
using CompGraphEngine.Engine;

namespace CompGraphEngine.Render
{
    public class Renderer2D : Renderer
    {
        public override void Draw(RenderObject ro)
        {
            ro._shader.SetMatrix4("aMVP", Camera.GetProjection2D(Constants.Width, Constants.Height) * Camera.GetViewMatrix() * ro.Model);
            ro.Draw();
        }
    }
}
