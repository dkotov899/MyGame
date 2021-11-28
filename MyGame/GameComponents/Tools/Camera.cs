using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MyGame.Input;
using MyGame.Sprites;

namespace MyGame.GameComponents.Tools
{
    public enum CameraMode { Free, Follow }

    public class Camera
    {
        private Vector2 _position;
        private float _speed;
        private float _zoom;
        private Rectangle _viewportRectangle;
        private CameraMode _mode;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set
            {
                _speed = (float)MathHelper.Clamp(_speed, 1f, 16f);
            }
        }

        public float Zoom
        {
            get { return _zoom; }
        }

        public CameraMode CameraMode
        {
            get { return _mode; }
        }

        public Matrix Transformation
        {
            get
            {
                return Matrix.CreateScale(_zoom) *
                    Matrix.CreateTranslation(new Vector3(-Position, 0f));
            }
        }

        public Rectangle ViewportRectangle
        {
            get
            {
                return new Rectangle(
                  _viewportRectangle.X,
                  _viewportRectangle.Y,
                  _viewportRectangle.Width,
                  _viewportRectangle.Height);
            }
        }

        public Camera(Rectangle viewportRect)
        {
            _speed = 4f;
            _zoom = 2.0f;
            _viewportRectangle = viewportRect;
            _mode = CameraMode.Follow;
        }

        public Camera(Rectangle viewportRect, Vector2 position)
        {
            _speed = 4f;
            _zoom = 2.0f;
            _viewportRectangle = viewportRect;
            Position = position;
            _mode = CameraMode.Follow;
        }

        public void Update(GameTime gameTime)
        {
            if (_mode == CameraMode.Follow)
            {
                return;
            }

            Vector2 motion = Vector2.Zero;

            if (InputHandler.KeyDown(Keys.Left) ||
                InputHandler.ButtonDown(Buttons.RightThumbstickLeft, PlayerIndex.One))
            {
                motion.X = -_speed;
            }
            else if (InputHandler.KeyDown(Keys.Right) ||
                InputHandler.ButtonDown(Buttons.RightThumbstickRight, PlayerIndex.One))
            {
                motion.X = _speed;
            }

            if (InputHandler.KeyDown(Keys.Up) ||
                InputHandler.ButtonDown(Buttons.RightThumbstickUp, PlayerIndex.One))
            {
                motion.Y = -_speed;
            }
            else if (InputHandler.KeyDown(Keys.Down) ||
                InputHandler.ButtonDown(Buttons.RightThumbstickDown, PlayerIndex.One))
            {
                motion.Y = _speed;
            }

            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                _position += motion * _speed;
            }
        }

        public void LockToSprite(AnimatedSprite sprite)
        {
            _position.X = (sprite.Position.X + sprite.Width / 2) * _zoom
                - (_viewportRectangle.Width / 2);

            _position.Y = (sprite.Position.Y + sprite.Height / 2) * _zoom
                - (_viewportRectangle.Height / 2);
        }

        public void ZoomIn()
        {
            #if DEBUG
            {
                _zoom += .25f;

                if (_zoom > 2.5f)
                    _zoom = 2.5f;

                Vector2 newPosition = Position * _zoom;

                SnapToPosition(newPosition);
            }
            #endif
        }

        public void ZoomOut()
        {
            #if DEBUG
            {
                _zoom -= .25f;

                if (_zoom < .5f)
                    _zoom = .5f;

                Vector2 newPosition = Position * _zoom;

                SnapToPosition(newPosition);
            }
            #endif
        }

        private void SnapToPosition(Vector2 newPosition)
        {
            _position.X = newPosition.X - _viewportRectangle.Width / 2;
            _position.Y = newPosition.Y - _viewportRectangle.Height / 2;
        }

        public void ToggleCameraMode()
        {
            if (_mode == CameraMode.Follow)
            {
                _mode = CameraMode.Free;
            }
            else if (_mode == CameraMode.Free)
            {
                _mode = CameraMode.Follow;
            }
        }
    }
}
