using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Mathematics;
using System.Collections.Generic;


namespace CompGraphEngine.Render.Model
{
    internal class Mesh
    {
        protected VertexBuffer<Vertex> _vertexBuffer;
        protected IndexBuffer _indexBuffer;
        protected VertexArray _vertexArray;
        protected VertexBufferLayout _vertexBufferLayout;


        public List<Vertex> vertices;
        public List<int> indices;
        public List<Texture> textures;

        public Mesh(List<Vertex> vertices, List<int> indices, List<Texture> textures)
        {
            this.vertices = vertices;
            this.indices = indices;
            this.textures = textures;

            setupMesh();
        }

        public void Draw(Shader shader)
        {
            int diffuseNr = 1;
            int specularNr = 1;

            for (uint i = 0; i < textures.Count; i++)
            {

            }
            
        }

        private void setupMesh()
        {
            _vertexBuffer = new VertexBuffer<Vertex>(vertices.ToArray(), vertices.Count * Vertex.Size());
            _indexBuffer = new IndexBuffer(indices.ToArray(), indices.Count);

            _vertexArray = new VertexArray();
            _vertexBufferLayout = new VertexBufferLayout();

            _vertexBufferLayout.Push<float>(3, false);
            _vertexBufferLayout.Push<float>(3, false);
            _vertexBufferLayout.Push<float>(2, false);


        }

       
    }
}
