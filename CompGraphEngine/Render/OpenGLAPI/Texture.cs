using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Render.OpenGLAPI
{
    internal class Texture : IDisposable
    {
        private readonly int id;
        private readonly int width;
        private readonly int height;
        private readonly string path;
        public Texture(string path)
        {
            id = GL.GenTexture();
            this.path = path;

            Bind();
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Image<Rgba32> image = Image.Load<Rgba32>(this.path);

            image.Mutate(x => x.Flip(FlipMode.Vertical));

            var pixels = new byte[4 * image.Width * image.Height];

            this.width = image.Width;
            this.height = image.Height;

            image.CopyPixelDataTo(pixels);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
        }

        public void Dispose()
        {
            Bind();
            GL.DeleteTexture(id);
            UnBind();
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, id);
        }



        public void UnBind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }


    }
}
