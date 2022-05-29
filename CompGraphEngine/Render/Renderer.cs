using CompGraphEngine.Engine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CompGraphEngine.Render
{
    public abstract class Renderer
    {
        public Camera Camera { get; set; }
        public abstract void Draw(RenderObject ro);
    
    }
}
