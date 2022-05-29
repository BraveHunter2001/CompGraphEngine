using CompGraphEngine.Engine;
using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CompGraphEngine.Render
{
    abstract class Renderer
    {
        internal Camera Camera { get; set; }
        internal abstract void Draw(RenderObject ro);
    
    }
}
