using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace CompGraphEngine.Render
{



    public class VertexBufferLayout
    {
        public struct VertexBufferElement
        {

            public int count;
            public VertexAttribPointerType type;
            public bool normalized;

            internal VertexBufferElement(VertexAttribPointerType type, int count, bool normalize)
            {
                this.count = count;
                this.type = type;
                this.normalized = normalize;
            }
        }
        //private struct OpenGlTypeSize
        //{
        //    public int size;
        //    public VertexAttribPointerType type;

        //    public OpenGlTypeSize(int size, VertexAttribPointerType type)
        //    {
        //        this.size = size;
        //        this.type = type;
        //    }

        //}

        public List<VertexBufferElement> Elements { get; private set; } = new List<VertexBufferElement>();
        public int Stride { get; private set; } = 0;

        public void Push<T>(int count)
        {
            VertexAttribPointerType typeSize = GetType<T>();
            VertexBufferElement vertexBufferElement = new VertexBufferElement(typeSize, count, false);
            Elements.Add(vertexBufferElement);
            Stride += GetSizeOpenGLType(typeSize) * count;
            
            
        }

        private VertexAttribPointerType GetType<T>()
        {
            
            System.Type typeG = typeof(T);

            if (typeG == typeof(int))
                return VertexAttribPointerType.Int;
            if (typeG == typeof(float))
                return  VertexAttribPointerType.Float;
            throw new System.Exception("Invalid type");

        }

        public static int GetSizeOpenGLType(VertexAttribPointerType type)
        {
            switch (type)
            {
                case VertexAttribPointerType.Int:
                    return sizeof(int);
                    break;
                case VertexAttribPointerType.Float:
                    return sizeof(float);
                    break;
               default:
                    throw new System.Exception("Error type");
            }
        }


    }
}