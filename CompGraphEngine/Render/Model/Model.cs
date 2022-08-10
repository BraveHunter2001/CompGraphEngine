using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;

namespace CompGraphEngine.Render.Model
{
    internal class Model
    {
        List<Mesh> meshes;
        string directory;

        public Model(string path)
        {
            LoadModel(path);
        }

        public Model(List<Mesh> meshes)
        {
            this.meshes = meshes;
        }

        public void Draw(Shader shader)
        {
            for (int i = 0; i < meshes.Count; i++)
                meshes[i].Draw(shader);
        }

        private void LoadModel(string path)
        {

        }

        public static int TextureFromFile (string path)
        {
            int id;
            id = GL.GenTexture();
            

            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Image<Rgba32> image = Image.Load<Rgba32>(path);

            image.Mutate(x => x.Flip(FlipMode.Vertical));

            var pixels = new byte[4 * image.Width * image.Height];


            image.CopyPixelDataTo(pixels);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return id;
        }
    }
}
