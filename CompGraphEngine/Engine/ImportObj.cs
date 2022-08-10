using CompGraphEngine.Render.Model;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CompGraphEngine.Engine
{
    internal static class ImportObj
    {
       
        public static Mesh ImportMesh(string path)
        {
            Mesh mesh = null;
            List<Vector3> position = new List<Vector3>();
            List<Vector2> texCoord = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();

            List<Face> faces = new List<Face>();

            
            string name = "";


            using (StreamReader r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    ParseLine(line, ref position, ref texCoord, ref normals, ref faces, ref name);
                }
            }

            var vertices = FillVertex(faces, position, texCoord, normals);

            mesh = new Mesh(vertices, faces, new List<Texture>());

            return mesh;
        }

        private static List<Vertex> FillVertex(List<Face> faces, List<Vector3> position, List<Vector2> texCoord, List<Vector3> normals)
        {
            List<Vertex> vertices = new List<Vertex>();

            

            for(int i = 0; i < faces.Count; i++)
            {
                Vertex v = new Vertex();
                v.Position = position[faces[i].positionIndex];
                v.TexCoords = texCoord[faces[i].textureIndex];
                v.Normal = normals[faces[i].normalIndex];
                vertices.Add(v);
            }

            return vertices;
        }

        private static void ParseLine(string line,
            ref List<Vector3> position,
            ref List<Vector2> texCoord,
            ref List<Vector3> normals,
            ref List<Face> faces,
            ref string name
            )
        {
            string[] parts = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) return;

            switch (parts[0])
            {
                case "v": ParseVertex(parts, ref position); break;
                case "vt": ParseTexCoord(parts, ref texCoord); break;
                case "vn": ParseNormal(parts, ref normals); break;
                case "f": ParseFace(parts, ref faces); break;
                case "o": ParseNameObject(parts, ref name); break;
                default: break;
            }

        }

        private static void ParseVertex(string[] line, ref List<Vector3> position)
        {
            Vector3 vertex = new Vector3();

            vertex.X = float.Parse(line[1], CultureInfo.InvariantCulture);
            vertex.Y = float.Parse(line[2], CultureInfo.InvariantCulture);
            vertex.Z = float.Parse(line[3], CultureInfo.InvariantCulture);

            position.Add(vertex);
        }
        private static void ParseTexCoord(string[] line, ref List<Vector2> texCoord)
        {
            Vector2 vertex = new Vector2();

            vertex.X = float.Parse(line[1], CultureInfo.InvariantCulture);
            vertex.Y = float.Parse(line[2], CultureInfo.InvariantCulture);


            texCoord.Add(vertex);
        }
        private static void ParseNormal(string[] line, ref List<Vector3> normals)
        {

            Vector3 vertex = new Vector3();

            vertex.X = float.Parse(line[1], CultureInfo.InvariantCulture);
            vertex.Y = float.Parse(line[2], CultureInfo.InvariantCulture);
            vertex.Z = float.Parse(line[3], CultureInfo.InvariantCulture);

            normals.Add(vertex);
        }
        private static void ParseFace(string[] line, ref List<Face> faces)
        {
            foreach (string s in line)
            {
                if (s.Equals("f"))
                    continue;

                Face face = new Face();


                if (s.Contains('/'))
                {
                    string[] ind;
                    ind = s.Split('/');
                    if (ind[0] != string.Empty)
                        face.positionIndex = int.Parse(ind[0]) - 1;
                    else
                        face.positionIndex = -1;

                    if (ind[1] != string.Empty)
                        face.textureIndex = int.Parse(ind[1]) - 1;
                    else
                        face.textureIndex = -1;

                    if (ind[2] != string.Empty)
                        face.normalIndex = int.Parse(ind[2]) - 1;
                    else
                        face.normalIndex = -1;
                }
                else
                {
                    face.positionIndex = int.Parse(s) - 1;
                    face.textureIndex = 0;
                    face.normalIndex = 0;
                }

                faces.Add(face);
              
            }
        }

        private static void ParseNameObject(string[] line, ref string name)
        {
            name = line[1];
        }
    }
}
