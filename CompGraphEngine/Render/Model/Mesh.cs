using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;


namespace CompGraphEngine.Render.Model
{
    enum TextureType
    {
        ambient,
        diffuse,
        specular,
    }
    struct Texture
    {
        public int id;
        public TextureType type;

        public Texture(int id, TextureType type)
        {
            this.id = id;
            this.type = type;
        }
    }
    struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TexCoords;
    }
    struct Face
    {
        public int positionIndex;
        public int textureIndex;
        public int normalIndex;

       public Face(int i)
        {
            positionIndex = i;
            textureIndex = i;
            normalIndex = i;
        }
    }

    struct Material
    {
        public string name;
        public Vector3 ambient;
        public Vector3 diffuse;
        public Vector3 specular;
        public float shininess;
        public List<Texture> Textures;

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
        public List<Material> materials;

        public string name;

        public Mesh(string name, List<Vertex> vertices, List<Face> faces, List<Material> materials = null)
        {
            this.vertices = vertices;
            this.faces = faces;
            this.textures = textures;
            this.name = name;
            this.materials = materials;

            setupMesh();
       
            //vertices.Clear();
        }


        public void Draw(Shader shader)
        {

            shader.Use();

            InitTextures(shader);
            InitMaterail(shader);
            //draw mesh

            _vertexArray.Bind();
            // _indexBuffer.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Count);
            //GL.DrawElements(BeginMode.Triangles, GetArrayPositionIndeces(faces).Length, DrawElementsType.UnsignedInt, 0);
            _vertexArray.UnBind();

            // _indexBuffer.UnBind();

            shader.Unuse();

        }

        private void InitTextures(Shader shader)
        {
            this.textures = new List<Texture>();

            foreach (var mat in this.materials)
                this.textures.AddRange(mat.Textures);


            int diffuseNr = 1;
            int specularNr = 1;

            if (textures == null || textures.Count == 0)
                return;

            for (int i = 0; i < textures.Count; i++)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i);

                string number = "";
                string name = textures[i].type.ToString();

                if (name == "diffuse")
                    number = (diffuseNr++).ToString();
                else if (name == "specular")
                    number = (specularNr++).ToString();

                name = "texture_" + name;
                shader.SetInt($"material{i}." + name, i);
                GL.BindTexture(TextureTarget.Texture2D, textures[i].id);
            }
            GL.ActiveTexture(TextureUnit.Texture0);
        }

        private void InitMaterail(Shader shader)
        {
            if (materials == null || materials.Count == 0)
                return;

            for (int i = 0; i < materials.Count; i++)
            {
                string name = materials[i].name;

                shader.SetVector3($"material{i}.ambient", materials[i].ambient);
                shader.SetVector3($"material{i}.diffuse", materials[i].diffuse);
                shader.SetVector3($"material{i}.specular", materials[i].specular);
                shader.SetFloat($"material{i}.shininess", materials[i].shininess);
            }

           
        }

        private void setupMesh()
        {
            var arr = toArrayFromListVertex(vertices);
           // var arrVertexIndex = GetArrayPositionIndeces(faces);

            _vertexBuffer = new VertexBuffer(arr, arr.Length * sizeof(float));
           // _indexBuffer = new IndexBuffer(arrVertexIndex, arrVertexIndex.Length);

            _vertexArray = new VertexArray();
            
            _vertexBufferLayout = new VertexBufferLayout();

            _vertexBufferLayout.Push<float>(3, true); // Position
            _vertexBufferLayout.Push<float>(3, true); // Normalies
            _vertexBufferLayout.Push<float>(2, true); // TexCoord

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

               // System.Console.WriteLine($"{result[shift * i]} {result[shift * i + 1]} {result[shift * i + 2]}|" +
                //    $" {result[shift * i + 3]} {result[shift * i + 4]} {result[shift * i + 5]}|" +
                //    $"{result[shift * i + 6]} {result[shift * i + 7]}");
            }

            

            return result;
        }

       
        private int[] GetArrayPositionIndeces(List<Face> faces)
        {
            int[] result = new int[faces.Count];

            for (int i = 0; i < result.Length; i++)
                result[i] = faces[i].positionIndex;

            return result;
        }

        public override string ToString()
        {
            string s = "";
            foreach (var n in materials)
                s += n.name + "|";
            return $"{name} - poligons: {vertices.Count / 3} [{s}] ";
        }

    }
}
