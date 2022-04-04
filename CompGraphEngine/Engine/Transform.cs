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
        private Vector4 position;
        private Vector3 rotation;
        private Vector3 scale;

        private Matrix4 model;
        private Matrix4 TranslateMatrix;
        private Matrix4 ScaleMatrix;

        private Matrix4 RotateXM;
        private Matrix4 RotateYM;
        private Matrix4 RotateZM;

        public Vector3 Position
        {
            get { return position.Xyz; }
            set { position.Xyz = value;
                TranslatePosition(value);
            }
        }
        public Vector3 Rotation
        {
            get { return rotation; }
            set { 
                rotation = value;
               RotateVector(value);
            }
        }
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value;
                Scaling(value);
            }
        }
        public Matrix4 Model { get 
            {
                model = TranslateMatrix * ScaleMatrix * RotateZM * RotateYM * RotateXM * Matrix4.Identity;
                Position = (model * position).Xyz;
                return model;
            } 
            private set { model = value; } }

        public Transform(Vector3 position, Vector3 scale, Vector3 rotation)
        {
            this.position = new Vector4(position, 1.0f);
            this.scale = scale;
            this.rotation = rotation;

            

            TranslateMatrix = Matrix4.CreateTranslation(this.position.X, this.position.Y, this.position.Z);
            ScaleMatrix = Matrix4.CreateScale(this.scale.X, this.scale.Y, this.scale.Z);

            RotateXM = Matrix4.CreateRotationX(this.rotation.X);
            RotateYM = Matrix4.CreateRotationY(this.rotation.Y);
            RotateZM = Matrix4.CreateRotationZ(this.rotation.Z);
        }
        public Transform()
        {
            this.position = new Vector4(0, 0, 0, 1);
            this.scale = new Vector3(1, 1, 1);
            this.rotation = new Vector3(0, 0, 0);

           

            TranslateMatrix = Matrix4.CreateTranslation(this.position.X, this.position.Y, this.position.Z);
            ScaleMatrix = Matrix4.CreateScale(this.scale.X, this.scale.Y, this.scale.Z);

            RotateXM = Matrix4.CreateRotationX(this.rotation.X);
            RotateYM = Matrix4.CreateRotationY(this.rotation.Y);
            RotateZM = Matrix4.CreateRotationZ(this.rotation.Z);
        }

        public void Translate(float x = 0, float y = 0, float z = 0)
        {
            Translate(new Vector3(x, y, z));
        }
        public void Translate(Vector3 translation)
        {
            Position += translation;
            
        }
        private void Rotate(float x = 0, float y = 0, float z = 0)
        {
           // Rotate(new Vector3(x,y,z));
        }
        private void RotateVector(Vector3 rotation)
        {
            this.rotation = rotation;
            RotateX(rotation.X);
            RotateY(rotation.Y);
            RotateZ(rotation.Z);
        }

        private void RotateX(float angle)
        {
            double radRadian = MathHelper.DegreesToRadians(angle);

            RotateXM.M22 = (float)MathHelper.Cos(radRadian);
            RotateXM.M23 = -(float)MathHelper.Sin(radRadian);
            RotateXM.M32 = (float)MathHelper.Sin(radRadian);
            RotateXM.M33 = (float)MathHelper.Cos(radRadian);

        }

        private void RotateY(float angle)
        {
            double radRadian = MathHelper.DegreesToRadians(angle);

            RotateYM.M11 = (float)MathHelper.Cos(radRadian);
            RotateYM.M13 = (float)MathHelper.Sin(radRadian);
            RotateYM.M31 = -(float)MathHelper.Sin(radRadian);
            RotateYM.M33 = (float)MathHelper.Cos(radRadian);

        }

        private void RotateZ(float angle)
        {
            double radRadian = MathHelper.DegreesToRadians(angle);

            RotateZM.M11 = (float)MathHelper.Cos(radRadian);
            RotateZM.M12 = -(float)MathHelper.Sin(radRadian);
            RotateZM.M21 = (float)MathHelper.Sin(radRadian);
            RotateZM.M22 = (float)MathHelper.Cos(radRadian);

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
