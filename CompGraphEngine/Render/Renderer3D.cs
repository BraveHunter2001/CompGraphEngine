using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render
{
    public class Renderer3D : Renderer 
    {
        public override void Draw(RenderObject ro)
        {
            ro._shader.SetMatrix4("aMVP", Camera.GetProjection3D() * Camera.GetViewMatrix() * ro.Model);
            ro.Draw();
        }
    }
}
