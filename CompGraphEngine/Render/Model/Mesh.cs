using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;


namespace CompGraphEngine.Render.Model
{
    struct Texture
    {
        public int id;
        public string type;
    }
    struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TexCoords;

       
    }

    struct Face
    {
        public int vertexIndex;
        public int normalIndex;
        public int textureIndex;
    }
    
    

    internal class Mesh
    {
        protected VertexBuffer _vertexBuffer;
        protected IndexBuffer _indexBuffer;
        protected VertexArray _vertexArray;
        protected VertexBufferLayout _vertexBufferLayout;


        public List<Vertex> vertices;
        public List<Face> faces;
        public List<Texture> textures;
        public string name;

        public Mesh(List<Vertex> vertices, List<Face> faces, List<Texture> textures)
        {
            this.vertices = vertices;
            this.faces = faces;
            this.textures = textures;

            setupMesh();
        }

        public Mesh(List<Vertex> vertices, List<int> vertexIndecies, List<Texture> textures)
        {
            this.vertices = vertices;
            this.textures = textures;

            faces = new List<Face>();

            foreach (var vind in vertexIndecies)
            {
                Face face = new Face();
                face.vertexIndex = vind;
                face.normalIndex = 0;
                face.textureIndex = 0;
                faces.Add(face);
            }
           

            setupMesh();
        }

        public void Draw(Shader shader)
        {
            int diffuseNr = 1;
            int specularNr = 1;
            shader.Use();
            for (int i = 0; i < textures.Count; i++)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i);

                string number = "";
                string name = textures[i].type;

                if (name == "texture_diffuse")
                    number = (diffuseNr++).ToString();
                else if (name == "texture_specular")
                    number = (specularNr++).ToString();

                shader.SetInt("material." + name + number, i);
                GL.BindTexture(TextureTarget.Texture2D, textures[i].id);
            }
            GL.ActiveTexture(TextureUnit.Texture0);

            //draw mesh
            
            _vertexArray.Bind();
            _indexBuffer.Bind();
           
            GL.DrawElements(PrimitiveType.Triangles, faces.Count, DrawElementsType.UnsignedInt, 0);
            _vertexArray.UnBind();
            _indexBuffer.UnBind();
            shader.Unuse();

        }

        private void setupMesh()
        {
            var arr = toArrayFromListVertex(vertices);
            var arrVertexIndex = GetArrayVertexIndex(faces);

            _vertexBuffer = new VertexBuffer(arr, arr.Length * sizeof(float));
            _indexBuffer = new IndexBuffer(arrVertexIndex, arrVertexIndex.Length);

            _vertexArray = new VertexArray();
            
            _vertexBufferLayout = new VertexBufferLayout();

            _vertexBufferLayout.Push<float>(3, false); // Position
            _vertexBufferLayout.Push<float>(3, false); // Normalies
            _vertexBufferLayout.Push<float>(2, false); // TexCoord

            _vertexArray.AddLayouts(ref _vertexBuffer, ref _vertexBufferLayout);

            _vertexArray.UnBind();
        }


        private float[] toArrayFromListVertex(List<Vertex> vertices)
        {
            int shift = 3 + 3 + 2; // count of Position Normalies TexCoord
            int len = vertices.Count * shift;
            float[] result = new float[len];

            for(int i = 0; i < vertices.Count; i++)
            {
                result[shift * i] = vertices[i].Position.X;
                result[shift * i + 1] = vertices[i].Position.Y;
                result[shift * i + 2] = vertices[i].Position.Z;

                result[shift * i + 3] = vertices[i].Normal.X;
                result[shift * i + 4] = vertices[i].Normal.Y;
                result[shift * i + 5] = vertices[i].Normal.Z;

                result[shift * i + 6] = vertices[i].TexCoords.X;
                result[shift * i + 7] = vertices[i].TexCoords.Y;

                System.Console.WriteLine($"{result[shift * i]} {result[shift * i + 1]} {result[shift * i + 2]}|" +
                    $" {result[shift * i + 3]} {result[shift * i + 4]} {result[shift * i + 5]}|" +
                    $"{result[shift * i + 6]} {result[shift * i + 7]}");
            }

            

            return result;
        }

        private int[] GetArrayVertexIndex(List<Face> faces)
        {
            int [] result = new int[faces.Count];
            int c = 0;
            foreach (Face face in faces)
            {
                result[c++] = face.vertexIndex;
            }

            return result;
        }

       
    }
}
