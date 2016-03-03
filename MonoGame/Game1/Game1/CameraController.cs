using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class CameraController
    {

        #region variables
        private Vector3 _camTarget;
        private Vector3 _camPosition;
        private Matrix _projectionMatrix;
        private Matrix _viewMatrix;
        private Matrix _worldMatrix;

        private bool _orbit;
        #endregion

        #region properties
        public Vector3 CamTarget
        {
            get { return _camTarget; }
            set { _camTarget = value; }
        }

        public Vector3 CamPosition
        {
            get { return _camPosition; }
            set { _camPosition = value; }
        }

        public Matrix ProjectionMatrix
        {
            get { return _projectionMatrix; }
            set { _projectionMatrix = value; }
        }

        public Matrix ViewMatrix
        {
            get { return _viewMatrix; }
            set { _viewMatrix = value; }
        }

        public Matrix WorldMatrix
        {
            get { return _worldMatrix; }
            set { _worldMatrix = value; }
        }

        public bool Orbit
        {
            get { return _orbit; }
            set { _orbit = value; }
        }
        #endregion

        #region methods

        public CameraController(Game game)
        {
            _camTarget = new Vector3(0, 0, 0);
            _camPosition = new Vector3(0, 0, -100);
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                game.GraphicsDevice.DisplayMode.AspectRatio,
                1, 1000);
            _viewMatrix = Matrix.CreateLookAt(_camPosition, _camTarget,
                Vector3.Up);
            _worldMatrix = Matrix.CreateWorld(_camTarget, Vector3.Forward,
                Vector3.Up);

            _orbit = false;
        }

        public void Update(GameTime gameTime)
        {
            //Player controls for moving camera
            CameraInput();

            //Camera orbiting
            if(_orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(
                    MathHelper.ToRadians(1f));
                _camPosition = Vector3.Transform(_camPosition, rotationMatrix);
            }

            _viewMatrix = Matrix.CreateLookAt(_camPosition, _camTarget, Vector3.Up);
        }

        void CameraInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _camPosition.X += 1f;
                _camTarget.X += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _camPosition.X -= 1f;
                _camTarget.X -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _camPosition.Z += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _camPosition.Z -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _orbit = !_orbit;
            }
        }

        #endregion
    }
}
