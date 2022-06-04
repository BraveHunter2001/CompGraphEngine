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
        public Vector4 PositionVec4
        {
            get { return position; }
            set
            {
                position= value;
                
            }
        }
        public Vector3 Rotation
        {
            get { return rotation; }
            set { 
                rotation = value;
               Rotate(value);
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

                
                return model;
            } 
            private set { model = value; } }

        

        public Transform(Vector3 position, Vector3 scale, Vector3 rotation)
        {
            this.position = new Vector4(position, 1.0f);
            this.scale = scale;
            this.rotation = rotation;


            TranslateMatrix = Matrix4.Identity;
            ScaleMatrix = Matrix4.Identity;

            RotateXM = Matrix4.Identity;
            RotateYM = Matrix4.Identity;
            RotateZM = Matrix4.Identity;

            TranslatePosition(this.position.Xyz);
            Scaling(this.scale);
            Rotate(this.rotation);
        }
        public Transform()
        {
            this.position = new Vector4(0, 0, 0, 1);
            this.scale = new Vector3(1, 1, 1);
            this.rotation = new Vector3(0, 0, 0);



            TranslateMatrix = Matrix4.Identity;
            ScaleMatrix = Matrix4.Identity;

            RotateXM = Matrix4.Identity;
            RotateYM = Matrix4.Identity;
            RotateZM = Matrix4.Identity;
            model = TranslateMatrix * RotateZM * RotateYM * RotateXM * ScaleMatrix * Matrix4.Identity;
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
            this.rotation = rotation;
            RotateX(rotation.X);
            RotateY(rotation.Y);
            RotateZ(rotation.Z);
            model = TranslateMatrix * RotateZM * RotateYM * RotateXM * ScaleMatrix * Matrix4.Identity;
        }

        /// <summary>
        /// First translate by axis on vector. Next rotate on degrees vector
        /// </summary>
        /// <param name="rotation"> Vector degrees roation axis</param>
        /// <param name="shift"> Vector translate axis</param>
        public void RotateWithShift(Vector3 shift, Vector3 rotation)
        {
            Rotate(rotation);
            Position = shift;

            model =  RotateZM * RotateYM * RotateXM *  TranslateMatrix * ScaleMatrix * Matrix4.Identity;
        }
       
        private void Scaling(Vector3 scale)
        {
            ScaleMatrix.M11 = scale.X;
            ScaleMatrix.M22 = scale.Y;
            ScaleMatrix.M33 = scale.Z;
            model = TranslateMatrix * RotateZM * RotateYM * RotateXM * ScaleMatrix * Matrix4.Identity;
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
            //TranslateMatrix.Transpose();
            model = TranslateMatrix * RotateZM * RotateYM * RotateXM * ScaleMatrix * Matrix4.Identity;
        }

        public Vector4 GetWorldPos()
        {
            return model * PositionVec4;
        }
       
        


        


    }
}
