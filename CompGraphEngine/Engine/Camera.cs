using OpenTK.Mathematics;

namespace CompGraphEngine.Engine
{
    public class Camera
    {
        public enum CameraMovement
        {
            FORWARD,
            BACKWARD,
            LEFT,
            RIGHT
        };

        const float PITCH = 0.0f;
        const float ZOOM = 45.0f;

        public Vector3 Position;
        public Vector3 Front;
        public Vector3 Up;
        public Vector3 Right;
        public Vector3 WorldUp;

        float yaw = -90.0f;
        float pitch = PITCH;

        public float Speed { get; set; } = 2.5f;
        public float Sensitivity { get; set; } = 0.1f;

        float zoom = ZOOM;

        public float Zoom
        {
            get { return zoom; }
            set
            {
                if (value > ZOOM)
                    zoom = ZOOM;
                else if (value < 1)
                    zoom = 1.0f;
                else
                    zoom = value;
            }
        }
        public float Pitch
        {
            get { return pitch; }
            set
            {

                pitch = value;

            }
        }
        public float Yaw
        {
            get { return yaw; }
            set { yaw = value; }
        }

        public Camera()
        {
            Position = new Vector3(0, 0, 0);
            WorldUp = new Vector3(0, 1, 0);
            updateCumeraVectors();
        }
        public Camera(Vector3 position, Vector3 up, Vector3 front)
        {
            Position = position;
            Front = front;
            Up = up;
            updateCumeraVectors();
        }

        public Matrix4 GetViewMatrix()
        {
            updateCumeraVectors();
            Matrix4 view = Matrix4.LookAt(Position, Position + Front, Up);
            view.Transpose();
            return view;
        }

        public Matrix4 GetProjection3D()
        {
            Matrix4 Projection = Matrix4.CreatePerspectiveFieldOfView(
                   MathHelper.DegreesToRadians(Zoom),
               Constants.Width / Constants.Height,
               0.1f, 1000.0f);
            Projection.Transpose();
            return Projection;
        }

        public Matrix4 GetProjection2D(float width, float height)
        {
            Matrix4 Projection = Matrix4.CreateOrthographic(width, height, 0.1f, 1000);
            Projection.Transpose();
            return Projection;
        }
        private void updateCumeraVectors()
        {
            Vector3 front = new Vector3();
            front.X = (float)MathHelper.Cos(MathHelper.DegreesToRadians(yaw))
                * (float)MathHelper.Cos(MathHelper.DegreesToRadians(pitch));

            front.Y = (float)MathHelper.Sin(MathHelper.DegreesToRadians(pitch));

            front.Z = (float)MathHelper.Sin(MathHelper.DegreesToRadians(yaw))
                * (float)MathHelper.Cos(MathHelper.DegreesToRadians(pitch));
            Front = front.Normalized();

            Right = Vector3.Cross(Front, WorldUp).Normalized();
            Up = Vector3.Cross(Right, Front).Normalized();
        }

        public void ProcessKeyboard(CameraMovement direction, float deltaTime)
        {
            float velocity = Speed * deltaTime;
            if (direction == CameraMovement.FORWARD)
                Position += Front * velocity;
            if (direction == CameraMovement.BACKWARD)
                Position -= Front * velocity;
            if (direction == CameraMovement.LEFT)
                Position -= Right * velocity;
            if (direction == CameraMovement.RIGHT)
                Position += Right * velocity;
        }
    }
}
