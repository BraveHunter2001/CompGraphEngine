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
       static Face MaxFace;
        public static Model ImportModel(string path)
        {
            Model model = null;
            MaxFace = new Face(0);

            List<Mesh> meshes = new List<Mesh>();
            int count = 0;

            using (StreamReader sr = new StreamReader(path))
            {
                string line;

                List<string> blockOfObj = new List<string>();


                while ((line = sr.ReadLine()) != null)
                {

                    string[] parts = ParseLine(line);

                    if (parts[0].Equals("#") || parts[0].Equals("mtllib"))
                        continue;

                    blockOfObj.Add(line);

                    if (parts[0].Equals("o"))
                    {

                        if (blockOfObj.Count > 1)
                        {
                            string nextname = blockOfObj[blockOfObj.Count - 1];
                            blockOfObj.RemoveAt(blockOfObj.Count - 1);
                            meshes.Add(ImportMesh(blockOfObj.ToArray(), MaxFace));
                            blockOfObj.Clear();
                            blockOfObj.Add(nextname);
                        }
                    }
                }

                meshes.Add(ImportMesh(blockOfObj.ToArray(), MaxFace));
                blockOfObj.Clear();

            }

            model = new Model(meshes, Path.GetFileNameWithoutExtension(path));

            return model;
        }

        private static Mesh ImportMesh(string[] blockOfObjFile, Face LastMaxFace)
        {
            Mesh mesh = null;

            List<Vector3> position = new List<Vector3>();
            List<Vector2> texCoord = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();
            List<Face> faces = new List<Face>();
            string name = "";


            foreach (var line in blockOfObjFile)
            {

                var parts = ParseLine(line);
                if (parts == null)
                    continue;

                switch (parts[0])
                {
                    case "v":
                        position.Add(ParseVertex3(parts));
                        break;

                    case "vt":
                        texCoord.Add(ParseVertex2(parts));
                        break;
                    case "vn":
                        normals.Add(ParseVertex3(parts));
                        break;

                    case "f":
                        ParseFace(parts, ref faces, LastMaxFace);
                        break;
                    case "o":
                        ParseNameObject(parts, ref name);
                        break;
                    default: break;
                }
            }


            MaxFace = GetMaxIndeciesOfFace(faces);

            var vertices = FillVertex(faces, position, texCoord, normals);

            mesh = new Mesh(name, vertices, faces);

            return mesh;
        }

        private static List<Vertex> FillVertex(List<Face> faces, List<Vector3> position, List<Vector2> texCoord, List<Vector3> normals)
        {
            List<Vertex> vertices = new List<Vertex>();



            for (int i = 0; i < faces.Count; i++)
            {
                Vertex v = new Vertex();
                v.Position = position[faces[i].positionIndex];
                if (texCoord.Count > 0)
                    v.TexCoords = texCoord[faces[i].textureIndex];
                else
                    v.TexCoords = new Vector2(0);
                if (normals.Count > 0)
                    v.Normal = normals[faces[i].normalIndex];
                else
                    v.Normal = new Vector3(0);
                vertices.Add(v);
            }

            return vertices;
        }

        private static Face GetMaxIndeciesOfFace(List<Face> faces)
        {
            Face max = new Face(0);
            foreach (Face face in faces)
            {
                if (face.positionIndex > max.positionIndex)
                    max.positionIndex = face.positionIndex;
            }

            foreach (Face face in faces)
            {
                if (face.textureIndex > max.textureIndex)
                    max.textureIndex = face.textureIndex;
            }

            foreach (Face face in faces)
            {
                if (face.normalIndex > max.normalIndex)
                    max.normalIndex = face.normalIndex;
            }

            max.positionIndex++;
            max.textureIndex++;
            max.normalIndex++;

            return max;
        }

        private static string[] ParseLine(string line)
        {
            string[] parts = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) return null;


            return parts;
        }

        private static Vector3 ParseVertex3(string[] line)
        {
            Vector3 vertex = new Vector3();

            vertex.X = float.Parse(line[1], CultureInfo.InvariantCulture);
            vertex.Y = float.Parse(line[2], CultureInfo.InvariantCulture);
            vertex.Z = float.Parse(line[3], CultureInfo.InvariantCulture);

            return vertex;
        }
        private static Vector2 ParseVertex2(string[] line)
        {
            Vector2 vertex = new Vector2();

            vertex.X = float.Parse(line[1], CultureInfo.InvariantCulture);
            vertex.Y = float.Parse(line[2], CultureInfo.InvariantCulture);

            return vertex;
        }

        private static void ParseFace(string[] line, ref List<Face> faces, Face lastMaxface)
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
                        face.positionIndex = int.Parse(ind[0]) - 1 - lastMaxface.positionIndex;
                    else
                        face.positionIndex = -1;

                    if (ind[1] != string.Empty)
                        face.textureIndex = int.Parse(ind[1]) - 1 -lastMaxface.textureIndex;
                    else
                        face.textureIndex = -1;

                    if (ind[2] != string.Empty)
                        face.normalIndex = int.Parse(ind[2]) - 1 -lastMaxface.normalIndex;
                    else
                        face.normalIndex = -1;
                }
                else
                {
                    face.positionIndex = int.Parse(s) - 1 - lastMaxface.positionIndex;
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
