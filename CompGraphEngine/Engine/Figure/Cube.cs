using CompGraphEngine.Render;
using CompGraphEngine.Render.OpenGLAPI;
using OpenTK.Mathematics;

namespace CompGraphEngine.Engine.Figure
{
    internal class Cube : GameObject
    {
        public Color4 color = Color4.Green;
        public Shader sh = new Shader("Shaders/cube.glsl");
        
        public Cube()
        {
            Transform = new Transform();
        }
        public override void Init()
        {
            var points = FillCoordsVertex();
            var colors = FillColorsVertex();
            var indeces = GenerateIndices();

            var normals = CalculateVertexNormals(points, indeces);

            var normalsV = Helper.ToVector3(normals);
            foreach(var n in normalsV)
             System.Console.WriteLine(n);
        

            renderObject = new RenderObjectsElements(points, colors,
                sh,
                Transform.Model,
                indeces,normals);

            renderObject.Init();
        }

        public override void Update()
        {
            renderObject.Model = Transform.Model;
            
            
        }

        private int[] GenerateIndices()
        {


            int[] inds = new int[] {2, 3, 4,
            8, 7, 6,
            5, 6, 2,
            6, 7, 3,
            3, 7, 8,
            1, 4, 8,
            1, 2, 4,
            5, 8, 6,
            1, 5, 2,
            2, 6, 3,
            4, 3, 8,
            5, 1, 8};


            for (int i = 0; i < inds.Length; i++)
                inds[i] -= 1;

            return inds;

        }

        float[,] FillCoordsVertex()
        {

            float[,] _vertPoints = new float[8, 3];

            _vertPoints[0, 0] = 1.0f;
            _vertPoints[0, 1] = -1.0f;
            _vertPoints[0, 2] = -1.0f;

            _vertPoints[1, 0] = 1.0f;
            _vertPoints[1, 1] = -1.0f;
            _vertPoints[1, 2] = 1.0f;

            _vertPoints[2, 0] = -1.0f;
            _vertPoints[2, 1] = -1.0f;
            _vertPoints[2, 2] = 1.0f;

            _vertPoints[3, 0] = -1.0f;
            _vertPoints[3, 1] = -1.0f;
            _vertPoints[3, 2] = -1.0f;

            _vertPoints[4, 0] = 1.0f;
            _vertPoints[4, 1] = 1.0f;
            _vertPoints[4, 2] = -1.0f;

            _vertPoints[5, 0] = 1.0f;
            _vertPoints[5, 1] = 1.0f;
            _vertPoints[5, 2] = 1.0f;

            _vertPoints[6, 0] = -1.0f;
            _vertPoints[6, 1] = 1.0f;
            _vertPoints[6, 2] = 1.0f;

            _vertPoints[7, 0] = -1.0f;
            _vertPoints[7, 1] = 1.0f;
            _vertPoints[7, 2] = -1.0f;



            return _vertPoints;
        }

        
        float[,] FillColorsVertex()
        {

            float[,] _vertColors = new float[8, 4];

            for (int i = 0; i < 8; i++)
            {
                _vertColors[i, 0] = color.R;
                _vertColors[i, 1] = color.G;
                _vertColors[i, 2] = color.B;
                _vertColors[i, 3] = color.A;

            }

            return _vertColors;
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
    }
}
