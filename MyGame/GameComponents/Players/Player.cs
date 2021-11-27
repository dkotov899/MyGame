using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.GameComponents.Tools;

using MyGame.Input;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.Components.Players
{
    public class Player
    {
        private Camera _camera;
        private MainGame _gameRef;
        private AnimatedSprite _sprite;

        private Vector2 _position;
        private Vector2 _positionOld;

        private Rectangle _playerBounds;

        private List<Rectangle> _collisionObjects;

        private bool _isAlive = true;

        public Camera Camera
        {
            get { return _camera; }
        }

        public AnimatedSprite Sprite
        {
            get { return _sprite; }
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public Rectangle PlayerBounds
        {
            get { return _playerBounds; }
        }

        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        public Player(Game game, Vector2 startPosition, List<Rectangle> collisionObjects)
        {
            _gameRef = (MainGame)game;
            _position = startPosition;
            _collisionObjects = collisionObjects;
        }

        public void LoadContent()
        {
            _camera = new Camera(_gameRef.ScreenRectangle, _position);

            _sprite = new AnimatedSprite(
                _gameRef.Content.Load<Texture2D>("Player/malerogue"),
                new Dictionary<AnimationKey, Animation>()
                {
                    { AnimationKey.Down, new Animation(3, 32, 32, 0, 0)  },
                    { AnimationKey.Left, new Animation(3, 32, 32, 0, 32) },
                    { AnimationKey.Right, new Animation(3, 32, 32, 0, 64) },
                    { AnimationKey.Up, new Animation(3, 32, 32, 0, 96) }
                });

            _sprite.Position = _position;

            _playerBounds = new Rectangle((int)_position.X + 8, (int)_position.Y + 16, 16, 16);
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);
            _camera.Update(gameTime);
            _camera.LockToSprite(_sprite);

            _positionOld = _position;

            if (InputHandler.KeyReleased(Keys.Up))
            {
                _camera.ZoomIn();

                if (_camera.CameraMode == CameraMode.Follow)
                {
                    _camera.LockToSprite(_sprite);
                }
            }
            else if (InputHandler.KeyReleased(Keys.Down))
            {
                _camera.ZoomOut();

                if (_camera.CameraMode == CameraMode.Follow)
                {
                    _camera.LockToSprite(_sprite);
                }
            }

            if (InputHandler.KeyDown(Keys.W))
            {
                _sprite.CurrentAnimation = AnimationKey.Up;

                if (CheckWorldCollisions(AnimationKey.Up) == true)
                {
                    _position.Y -= _sprite.Speed;
                }
            }

            if (InputHandler.KeyDown(Keys.S))
            {
                _sprite.CurrentAnimation = AnimationKey.Down;

                if (CheckWorldCollisions(AnimationKey.Down) == true)
                {
                    _position.Y += _sprite.Speed;
                }
            }

            if (InputHandler.KeyDown(Keys.A))
            {
                _sprite.CurrentAnimation = AnimationKey.Left;

                if (CheckWorldCollisions(AnimationKey.Left) == true)
                {
                    _position.X -= _sprite.Speed;
                }
            }

            if (InputHandler.KeyDown(Keys.D))
            {
                _sprite.CurrentAnimation = AnimationKey.Right;

                if (CheckWorldCollisions(AnimationKey.Right) == true)
                {
                    _position.X += _sprite.Speed;
                }
            }

            if (_position != _positionOld)
            {
                _sprite.IsAnimating = true;

                _position.Normalize();

                _sprite.Position += _position * _sprite.Speed;

                _playerBounds.X = (int)_sprite.Position.X + 8;
                _playerBounds.Y = (int)_sprite.Position.Y + 16;
            }
            else
            {
                _sprite.IsAnimating = false;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(gameTime, spriteBatch);
        }

        private bool CheckWorldCollisions(AnimationKey key)
        {
            var _playerBoundsTest = _playerBounds;

            if (key == AnimationKey.Up)
            {
                _playerBoundsTest.X = _playerBounds.X;
                _playerBoundsTest.Y = (int)(_playerBounds.Y - 4);
            }
            
            if (key == AnimationKey.Down)
            {
                _playerBoundsTest.X = _playerBounds.X;
                _playerBoundsTest.Y = (int)(_playerBounds.Y + 4);
            }
            
            if (key == AnimationKey.Left)
            {
                _playerBoundsTest.X = (int)_playerBounds.X - 8;
                _playerBoundsTest.Y = _playerBounds.Y;
            }
            
            if (key == AnimationKey.Right)
            {
                _playerBoundsTest.X = (int)_playerBounds.X + 8;
                _playerBoundsTest.Y = _playerBounds.Y;
            }
            
            foreach (var rect in _collisionObjects)
            {
                if (rect.Intersects(_playerBoundsTest) == true)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
