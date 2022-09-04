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
        static Face prevMaxFace;


        public static Model ImportModel(string path)
        {
            Model model = null;
            MaxFace = new Face(0);
            prevMaxFace = new Face(0);

            List<Mesh> meshes = new List<Mesh>();
            List<Material> materials = new List<Material>();
            


            using (StreamReader sr = new StreamReader(path))
            {
                string line;

                List<string> blockOfObj = new List<string>();
                while ((line = sr.ReadLine()) != null)
                {
                    
                    string[] parts = ParseLine(line);

                    if (parts[0].Equals("#"))
                        continue;

                    if (parts[0].Equals("mtllib"))
                    {
                        int ind = path.LastIndexOf('\\');
                        string pathToMTL = path.Substring(0, ind) +"\\"+ parts[1];
                        materials = ImportMTL(pathToMTL);
                        continue;
                    }

                    blockOfObj.Add(line);

                    if (parts[0].Equals("o"))
                    {

                        if (blockOfObj.Count > 1)
                        {
                            string nextname = blockOfObj[blockOfObj.Count - 1];
                            blockOfObj.RemoveAt(blockOfObj.Count - 1);
                            meshes.Add(ImportMesh(blockOfObj.ToArray()));
                            blockOfObj.Clear();
                            blockOfObj.Add(nextname);
                        }
                    }
                }

                

                meshes.Add(ImportMesh(blockOfObj.ToArray()));
                blockOfObj.Clear();

            }

            foreach(var mesh in meshes)
            {
                for(int i = 0; i < mesh.materials.Count; i++)
                    foreach(var mat in materials)
                    {
                        if (mesh.materials[i].name == mat.name)
                        {
                            mesh.materials[i] = mat;
                        }
                    }
            }


            model = new Model(meshes, Path.GetFileNameWithoutExtension(path));

            return model;
        }

        public static List<Material> ImportMTL(string path)
        {
            List<Material> materials = new List<Material>();
            
            using (StreamReader sr = new StreamReader(path))
            {
                string line;

                List<string> blockOfObj = new List<string>();
                while ((line = sr.ReadLine()) != null)
                {

                    string[] parts = ParseLine(line);

                    if (parts == null || parts[0].Equals("#"))
                        continue;

                    blockOfObj.Add(line);

                    if (parts[0].Equals("newmtl"))
                    {

                        if (blockOfObj.Count > 1)
                        {
                            string nextname = blockOfObj[blockOfObj.Count - 1];
                            blockOfObj.RemoveAt(blockOfObj.Count - 1);
                            materials.Add(ImportMaterial(blockOfObj.ToArray()));

                            blockOfObj.Clear();
                            blockOfObj.Add(nextname);
                        }
                    }
                }

                materials.Add(ImportMaterial(blockOfObj.ToArray()));
                blockOfObj.Clear();
            }
            return materials;
        }

        public static Material ImportMaterial(string[] blockOfObjFile)
        {
            Material material = new Material();
            foreach (string line in blockOfObjFile)
            {
                var parts = ParseLine(line);

                if (parts == null)
                    continue;

                switch (parts[0])
                {
                    case "newmtl":
                        material.name  = parts[1];
                        break;

                    case "Ns":
                        material.shininess = float.Parse(parts[1],  CultureInfo.InvariantCulture);
                        break;

                    case "Ka":
                        material.ambient = ParseVertex3(parts);
                        break;

                    case "Kd":
                        material.diffuse = ParseVertex3(parts);
                        break;

                    case "Ks":
                        material.specular = ParseVertex3(parts);
                        break;

                    default: break;
                }

            }
            return material;
        }

        private static Mesh ImportMesh(string[] blockOfObjFile)
        {
            Mesh mesh = null;
            
            List<Vector3> position = new List<Vector3>();
            List<Vector2> texCoord = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();
            List<Face> faces = new List<Face>();
            List<Material> materials = new List<Material>();
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
                        ParseFace(parts, ref faces);
                        break;
                    case "usemtl":
                        var mat = new Material();
                        mat.name = parts[1];
                        materials.Add(mat);
                        break;
                    case "o":
                        ParseNameObject(parts, ref name);
                        break;
                    default: break;
                }
            }


            faces = NextFaceIndecies(faces);
            prevMaxFace = MaxFace;

           
            var vertices = FillVertex(faces, position, texCoord, normals);

            mesh = new Mesh(name, vertices, faces);
            mesh.materials = materials; 

            return mesh;
        }

        private static List<Vertex> FillVertex(List<Face> faces, List<Vector3> position, List<Vector2> texCoord, List<Vector3> normals)
        {
            List<Vertex> vertices = new List<Vertex>();
            
            for (int i = 0; i < faces.Count; i++)
            {
                Vertex v = new Vertex();
                v.Position = position[faces[i].positionIndex - 1];
                if (texCoord.Count > 0)
                    v.TexCoords = texCoord[faces[i].textureIndex  - 1];
                else
                    v.TexCoords = new Vector2(0);
                if (normals.Count > 0)
                    v.Normal = normals[faces[i].normalIndex - 1];
                else
                    v.Normal = new Vector3(0);
                vertices.Add(v);
            }
            
            return vertices;
        }

        private static List<Face> NextFaceIndecies(List<Face> faces)
        {
            List<Face> facesNew = new List<Face>();
             for (int i = 0; i < faces.Count; i++)
            {
                Face face = new Face();
               face.positionIndex = faces[i].positionIndex - prevMaxFace.positionIndex;
                face.textureIndex = faces[i].textureIndex - prevMaxFace.textureIndex;
                face.normalIndex = faces[i].normalIndex - prevMaxFace.normalIndex;
                facesNew.Add(face);
            }
            return facesNew;
           
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
                    {
                        face.positionIndex = int.Parse(ind[0]);
                        if (int.Parse(ind[0]) > MaxFace.positionIndex)
                            MaxFace.positionIndex = int.Parse(ind[0]);
                    }                        
                    else
                        face.positionIndex = -1;

                    if (ind[1] != string.Empty) 
                    {
                        face.textureIndex = int.Parse(ind[1]);
                        if (MaxFace.textureIndex < int.Parse(ind[1]))
                            MaxFace.textureIndex = int.Parse(ind[1]);
                    }
                    else
                        face.textureIndex = -1;

                    if (ind[2] != string.Empty)
                    {
                        face.normalIndex = int.Parse(ind[2]);
                        if (MaxFace.normalIndex < int.Parse(ind[2]))
                            MaxFace.normalIndex = int.Parse(ind[2]);
                    }
                    else
                        face.normalIndex = -1;
                }
                else
                {
                    face.positionIndex = int.Parse(s);
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
