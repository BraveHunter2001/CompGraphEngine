using CompGraphEngine.Render;
using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine.Figure
{

    internal class Surface : GameObject
    {

        public Shader sh = new Render.OpenGLAPI.Shader("Shaders/cube.glsl");



        public Color4 color = Color4.Red;

        public Surface()
        {
            Transform = new Transform();
        }
        public override void Init()
        {
            int row = 100, col = 100;
            
            var points = FillCoordsVertex(row, col);

            points = Normalize(points);
            var colors = FillColorsVertex(row, col);
            var indeces = GenerateIndices(row, col);
            var normals = CalculateVertexNormals(points, indeces);

            renderObject = new RenderObjectsElements(points, colors,
                sh,
                Transform.Model, indeces
                , normals);

            renderObject.Init();

        }
        float[,] CalculateVertexNormals(float[,] vertexPos, int[] triangleIndices)
        {
            Vector3[] vertexNormals = new Vector3[vertexPos.GetLength(0)];
            Vector3[] vertexPositions = Helper.ToVector3(vertexPos);
            // Zero-out our normal buffer to start from a clean slate.
            for (int vertex = 0; vertex < vertexPositions.Length; vertex++)
                vertexNormals[vertex] = Vector3.Zero;

            // For each face, compute the face normal, and accumulate it into each vertex.
            for (int index = 0; index < triangleIndices.Length; index += 3)
            {
                int vertexA = triangleIndices[index];
                int vertexB = triangleIndices[index + 1];
                int vertexC = triangleIndices[index + 2];

                var edgeAB = vertexPositions[vertexB] - vertexPositions[vertexA];
                var edgeAC = vertexPositions[vertexC] - vertexPositions[vertexA];

                // The cross product is perpendicular to both input vectors (normal to the plane).
                // Flip the argument order if you need the opposite winding.    
                var areaWeightedNormal = Vector3.Cross(edgeAB, edgeAC);

                // Don't normalize this vector just yet. Its magnitude is proportional to the
                // area of the triangle (times 2), so this helps ensure tiny/skinny triangles
                // don't have an outsized impact on the final normal per vertex.

                // Accumulate this cross product into each vertex normal slot.
                vertexNormals[vertexA] += areaWeightedNormal;
                vertexNormals[vertexB] += areaWeightedNormal;
                vertexNormals[vertexC] += areaWeightedNormal;
            }

            // Finally, normalize all the sums to get a unit-length, area-weighted average.
            for (int vertex = 0; vertex < vertexPositions.Length; vertex++)
                vertexNormals[vertex].Normalize();

            return Helper.ToFloatArr(vertexNormals);
        }

        // this shit
        private int[] GenerateIndices(int row, int col)
        {
            

            List<int> indexes = new List<int>();
            
            int t = -1;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < col - 1; j++)
                {
                    indexes.Add(i * col + j);
                    indexes.Add(i * col + (j + 1) );
                    indexes.Add((i + 1) * col + j );

                    indexes.Add((i + 1) * col + j );
                    indexes.Add(i * col + (j + 1) );
                    indexes.Add((i + 1) * col + (j + 1));


                    //_indexes[++t] = i*col + j;
                    //_indexes[++t] = i*col + (j + 1);
                    //_indexes[++t] = (i+1)*col + j;

                    //_indexes[++t] = (i + 1)*col + j;
                    //_indexes[++t] = i * col + (j + 1);
                    //_indexes[++t] = (i+1) * col + (j + 1);
                }
            }

            return indexes.ToArray();

        }

        float[,] FillCoordsVertex(int countRowVert, int countColVert)
        {
            float[,] _vertPoints = new float[countRowVert * countColVert, 3];
            int t = 0;
            int si = 0;
            for (int i = 0; i < countRowVert; i++)
            {
                for (int j = 0; j < countColVert; j++)
                {
                    _vertPoints[t, 0] = i * 0.5f;
                    _vertPoints[t, 1] = new Random().Next(0,10) * 0.02f;
                    _vertPoints[t, 2] = j * 0.5f;
                    t++;
                }
                si++;
            }
            return _vertPoints;
        }

        float[,] Normalize(float[,] arr)
        {
            float[,] res = new float[arr.GetLength(0) , 3];

            float MaxX = arr[0,0], MaxY = arr[0, 1], MaxZ = arr[0, 2];

            for (int j = 0; j < arr.GetLength(0); j++)
            {
                if (arr[j, 0] > MaxX)
                    MaxX = arr[j, 0];
                if (arr[j, 1] > MaxY)
                    MaxY = arr[j, 1];
                if (arr[j, 2] > MaxZ)
                    MaxZ = arr[j, 2];

            }

            for (int j = 0; j < arr.GetLength(0); j++)
            {
                res[j, 0] = arr[j, 0] - MaxX / 2 ;
                res[j, 1] = arr[j, 1] - MaxY / 2;
                res[j, 2] = arr[j, 2] - MaxZ / 2;
            }

            return res;
        }
        float[,] FillColorsVertex(int countRowVert, int countColVert)
        {
            float[,] _vertColors = new float[countRowVert * countColVert, 4];
            int count = 0;

            for (int i = 0; i < countRowVert; i++)
            {
                for (int j = 0; j < countColVert; j++)
                {
                    _vertColors[count, 0] = color.R;
                    _vertColors[count, 1] = color.G;
                    _vertColors[count, 2] = color.B;
                    _vertColors[count, 3] = color.A;
                    count++;
                }
                
            }
            return _vertColors;
        }

        public override void Update()
        {
            renderObject.Model = Transform.Model;
        }
    }
}
