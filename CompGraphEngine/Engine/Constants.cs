using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine
{
    internal static class Constants
    {
        public static int Height = 800;
        public static int Width = 800;

    }

    public static class Helper
    {
        public static Vector2 GetWindowPosObj(Vector3 positionOnWorld, Camera cam)
        {
            Vector4 posW = new Vector4(positionOnWorld);
            posW.W = 1;
            Vector4 clipSpacePos = cam.GetProjection3D() * cam.GetViewMatrix() * posW;
            Vector3 ndcSpacePos = clipSpacePos.Xyz / clipSpacePos.W;
            Vector2 viewSize = new Vector2(Constants.Width, Constants.Height);


            Vector2 windowSpacePos = new Vector2((float)((ndcSpacePos.X + 1.0) / 2f) * viewSize.X, (float)((1.0 - ndcSpacePos.Y) / 2.0f) * viewSize.Y);

            return windowSpacePos;
        }

        public static Vector3 GetNormalbyPoligon(Vector3 A, Vector3 B, Vector3 C)
        {
            var side1 = B - A;
            var side2 = C - A;
            var normal = Vector3.Cross(side2, side1);
            normal.Normalize();

            return normal;
        }

        public static Vector3[] ToVector3(float[,] arr)
        {
            Vector3[] res = new Vector3[arr.GetLength(0)];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new Vector3(arr[i, 0], arr[i, 1], arr[i, 2]);
            }
            return res;
        }
        public static float[,] ToFloatArr(Vector3[] arr)
        {
            float[,] res = new float[arr.Length, 3];

            for (int i = 0; i < arr.Length; i++)
            {
                res[i, 0] = arr[i].X;
                res[i, 1] = arr[i].Y;
                res[i, 2] = arr[i].Z;
            }
            return res;
        }
    }
}
