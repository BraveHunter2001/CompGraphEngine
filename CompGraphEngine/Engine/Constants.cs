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
        public static int Height = 600;
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
    }
}
