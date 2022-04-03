using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine
{
    public class Camera
    {

        public Camera()
        {
            Matrix4 Projection = Matrix4.CreateOrthographic(800, 600, 0f, 1000f);

            Matrix4 View = Matrix4.LookAt(new Vector3(0f, 0f, 10f),
                new Vector3(0f, 0, 0),
                new Vector3(0, 1, 0));

        }
    }
}
