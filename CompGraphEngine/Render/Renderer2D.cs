
using CompGraphEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    internal class Renderer2D : Renderer
    {
        internal override void Draw(RenderObject ro)
        {
            ro._shader.SetMatrix4("aMVP", Camera.GetProjection2D(Constants.Width, Constants.Height) * Camera.GetViewMatrix() * ro.Model);
            ro.Draw();
        }
    }
}
