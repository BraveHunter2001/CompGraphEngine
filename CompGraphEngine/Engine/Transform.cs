using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace CompGraphEngine.Engine
{
    public class Transform
    {
        private Vector3 position;
        private Vector3 rotation;
        private Vector3 scale;

        private Matrix4 model;
        private Matrix4 TranslateMatrix;
        private Matrix4 ScaleMatrix;

        public Vector3 Position
        {
            get { return position; }
            set { position = value;
                TranslatePosition(value);
            }
        }
        public Vector3 Rotation
        {
            get { return rotation; }
            set { 
                rotation = value;
                model = Matrix4.CreateRotationX(this.rotation.X) * model;
                model = Matrix4.CreateRotationY(this.rotation.Y) * model;
                model = Matrix4.CreateRotationZ(this.rotation.Z) * model;
            }
        }
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value;
                Scaling(value);
            }
        }
        public Matrix4 Model { get { return TranslateMatrix * ScaleMatrix * model; } private set { model = value; } }
        

        public Transform()
        {
            position = new Vector3(0, 0, 0);
            scale = new Vector3(1, 1, 1);
            rotation = new Vector3(0, 0, 0);

            model = Matrix4.Identity;
            TranslateMatrix = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            ScaleMatrix = Matrix4.CreateScale(scale.X, scale.Y, scale.Z);

            

        }

        public void Translate(float x = 0, float y = 0, float z = 0)
        {
            Translate(new Vector3(x, y, z));
        }
        public void Translate(Vector3 translation)
        {
            Position += translation;
            
        }
        public void Rotate(float x = 0, float y = 0, float z = 0)
        {
            Rotate(new Vector3(x,y,z));
        }
        public void Rotate(Vector3 rotation)
        {
           
        }

        private void TranslatePosition(Vector3 pos)
        {
            TranslateMatrix.M14 = pos.X;
            TranslateMatrix.M24 = pos.Y;
            TranslateMatrix.M34 = pos.Z;
        }

        private void Scaling(Vector3 scale)
        {
            ScaleMatrix.M11 = scale.X;
            ScaleMatrix.M22 = scale.Y;
            ScaleMatrix.M33 = scale.Z;
        }

    }
}
