﻿using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace CompGraphEngine.Render.OpenGLAPI
{

    public class VertexBufferLayout
    {
        public struct VertexBufferElement
        {

            public int count;
            public VertexAttribPointerType type;
            public bool normalized;

            public VertexBufferElement(VertexAttribPointerType type, int count, bool normalize)
            {
                this.count = count;
                this.type = type;
                normalized = normalize;
            }
        }

        public List<VertexBufferElement> Elements { get; private set; } = new List<VertexBufferElement>();
        public int Stride { get; private set; } = 0;

        public void Push<T>(int count, bool isNormalized) where T : struct
        {
            VertexAttribPointerType typeSize = GetType<T>();
            VertexBufferElement vertexBufferElement = new VertexBufferElement(typeSize, count, !isNormalized);
            Elements.Add(vertexBufferElement);
            Stride += GetSizeOpenGLType(typeSize) * count;


        }

        //TODO Fuck this code
        private static VertexAttribPointerType GetType<T>()
        {

            System.Type typeG = typeof(T);

            if (typeG == typeof(int))
                return VertexAttribPointerType.Int;
            if (typeG == typeof(float))
                return VertexAttribPointerType.Float;
            throw new System.Exception("Invalid type");

        }
        //TODO Fuck this code
        internal static int GetSizeOpenGLType(VertexAttribPointerType type)
        {
            switch (type)
            {
                case VertexAttribPointerType.Int:
                    return sizeof(int);

                case VertexAttribPointerType.Float:
                    return sizeof(float);

                default:
                    throw new System.Exception("Error type");
            }
        }


    }
}