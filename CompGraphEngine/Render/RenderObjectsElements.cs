﻿using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace CompGraphEngine.Render
{
    public class RenderObjectsElements: RenderObject
    {
        IndexBuffer _indexBuffer;
        Texture t;


        public RenderObjectsElements(float[,] Points, float[,] Colors, Shader shader, Matrix4 model, int[] Indexes)
        {
            _vertPoints = Points;
            _vertColors = Colors;
            _shader = shader;
            Model = model;
            _indexBuffer = new IndexBuffer(Indexes, Indexes.Length);

        }

        public RenderObjectsElements(float[,] Points, float[,] Colors, Shader shader, Matrix4 model, int[] Indexes, float[,] texCoord)
        {
            _vertPoints = Points;
            _vertColors = Colors;
            _shader = shader;
            Model = model;
            _TexCoord = texCoord;
            _indexBuffer = new IndexBuffer(Indexes, Indexes.Length);
            t = new Texture(@"C:\Users\Ilya\Source\Repos\BraveHunter2001\CompGraphEngine\CompGraphEngine\Shaders\wall.jpg");

        }

        
        //TODO: shit
        public override void Init()
        {
            base.Init();
          //   _layoutNorm = new VertexBufferLayout();
          //  _normalBuffer = new VertexBuffer(Make1DArray(_vertNormals), sizeof(float) * _vertNormals.Length);
         //   _layoutNorm.Push<float>(_vertNormals.GetLength(1), false);
          //  _vertexArray.AddLayout(ref _normalBuffer, ref _layoutNorm, 2);
        }
        public override void Draw()
        {
            _vertexArray.Bind();
            _indexBuffer.Bind();
            t.Bind();
            _shader.Use();
            GL.DrawElements(PrimitiveType.Triangles, _indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
        }

    }
}
