﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;


namespace CompGraphEngine.Render.OpenGLAPI
{
    public class Shader : IDisposable
    {
        public readonly int Handle;
        private Dictionary<string, int> _uniformLocations;
        private struct ShaderProgramSource
        {
            public string vertexSource;
            public string fragmentSource;
            public string geometrySource;
        }

        public Shader(string filePath)
        {

            ShaderProgramSource shaderProgramSource = ParserShader(filePath);

            //Console.WriteLine("Fragment:");
            //Console.WriteLine(shaderProgramSource.fragmentSource);
            //Console.WriteLine("Vertex:");
            //Console.WriteLine(shaderProgramSource.vertexSource);

            // load and compile vertex shader;

            var vertexShader = GL.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderProgramSource.vertexSource);
            CompileShader(vertexShader);

            // load and compile fragment shader;

            var fragmentShader = GL.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderProgramSource.fragmentSource);
            CompileShader(fragmentShader);

          

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
           
            LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            

            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
            

            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);


            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(Handle, i, out _, out _);
                var location = GL.GetUniformLocation(Handle, key);

                _uniformLocations.Add(key, location);
            }
        }

        public Shader(string filePath, bool geometry)
        {

            ShaderProgramSource shaderProgramSource = ParserShader(filePath);

            //Console.WriteLine("Fragment:");
            //Console.WriteLine(shaderProgramSource.fragmentSource);
            //Console.WriteLine("Vertex:");
            //Console.WriteLine(shaderProgramSource.vertexSource);

            // load and compile vertex shader;

            var vertexShader = GL.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderProgramSource.vertexSource);
            CompileShader(vertexShader);

            // load and compile fragment shader;

            var fragmentShader = GL.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderProgramSource.fragmentSource);
            CompileShader(fragmentShader);

            var geometryShader = GL.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.GeometryShader);
            GL.ShaderSource(geometryShader, shaderProgramSource.geometrySource);
            CompileShader(geometryShader);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.AttachShader(Handle, geometryShader);
            LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DetachShader(Handle, geometryShader);

            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(geometryShader);

            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);


            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(Handle, i, out _, out _);
                var location = GL.GetUniformLocation(Handle, key);

                _uniformLocations.Add(key, location);
            }
        }
        private enum ShaderType
        {
            VertexShader, FragmentShader, geometryShader, None
        }
        private static ShaderProgramSource ParserShader(string filePath)
        {
            ShaderProgramSource programSource;
            ShaderType type = ShaderType.None;

            programSource.vertexSource = "";
            programSource.fragmentSource = "";
            programSource.geometrySource = ""; 

            using (var stream = new StreamReader(filePath))
            {
                string line;

                while ((line = stream.ReadLine()) != null)
                {
                    //TODO Delete empty strings in shader file

                    if (line.Equals("#type vertex"))
                    {
                        type = ShaderType.VertexShader;
                    }
                    else if (line.Equals("#type fragment"))
                    {
                        type = ShaderType.FragmentShader;
                    }
                    else if (line.Equals("#type geometry"))
                    {
                        type = ShaderType.geometryShader;
                    }
                    else
                    {
                        if (type == ShaderType.VertexShader)
                        {
                            programSource.vertexSource += line + "\n";
                        }
                        else if (type == ShaderType.FragmentShader)
                        {
                            programSource.fragmentSource += line + "\n";
                        }
                        else if (type == ShaderType.geometryShader)
                        {
                            programSource.geometrySource += line + "\n";
                        }
                    }
                }
            }

            return programSource;
        }
        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($" Error occured whilst compiling Shader({shader})\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var log = GL.GetProgramInfoLog(program);
                throw new Exception($"Error occure whilst linking Program({program})\n\n{log}");
            }
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }
        public void Unuse()
        {
            GL.UseProgram(0);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }
        /// <summary>
        /// Set a uniform int on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        /// <summary>
        /// Set a uniform float on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        /// <summary>
        /// Set a uniform Matrix4 on this shader
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        /// <remarks>
        ///   <para>
        ///   The matrix is transposed before being sent to the shader.
        ///   </para>
        /// </remarks>
        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        /// <summary>
        /// Set a uniform Vector3 on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(_uniformLocations[name], data);
        }
        public void SetVector2(string name, Vector2 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform2(_uniformLocations[name], data);
        }

        public void Dispose()
        {

            GL.DeleteProgram(Handle);

        }
    }
}
