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

        public Vector3 Position
        {
            get { return position; }
            set { position = value; 
                Matrix4 transl = Matrix4.CreateTranslation(position);
                transl.Transpose();
                model = transl * model;
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
                model = Matrix4.CreateScale(scale.X, scale.Y, scale.Z) * model;
            }
        }
        public Matrix4 Model { get { return model; } private set { model = value; } }

        public Transform()
        { 
            model = Matrix4.Identity;
            position = new Vector3(0, 0, 0);
            scale = new Vector3(1, 1, 1);
            rotation = new Vector3(0, 0, 0);

        }

        public void Translate(float x = 0, float y = 0, float z = 0)
        {
            Translate(new Vector3(x, y, z));
        }
        public void Translate(Vector3 translation)
        {
            this.position += translation;
            Matrix4 transl = Matrix4.CreateTranslation(translation);
            transl.Transpose();
            model = transl * model;
        }

        public void Rotate(float x = 0, float y = 0, float z = 0)
        {
            Rotate(new Vector3(x,y,z));
        }
        public void Rotate(Vector3 rotation)
        {
            this.rotation += rotation;
            model = Matrix4.CreateRotationX(this.rotation.X) * model;
            model = Matrix4.CreateRotationY(this.rotation.Y) * model;
            model = Matrix4.CreateRotationZ(this.rotation.Z) * model;
        }
        
        

    }
}
