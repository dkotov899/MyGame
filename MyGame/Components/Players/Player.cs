using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Input;
using MyGame.Sprites;
using System;
using System.Collections.Generic;

namespace MyGame.Components.Players
{
    public class Player
    {
        private MainGame _gameRef;
        private AnimatedSprite _sprite;

        private Rectangle _playerBounds;

        private Vector2 _position;
        private Vector2 _positionOld;

        private List<Rectangle> _collisionObjects;
        private Dictionary<int, Rectangle> _locksObjects;
        private Dictionary<int, Rectangle> _receivedLocksObjects;

        private Texture2D _pRect;

        public Rectangle PlayerBounds
        {
            get { return _playerBounds; }
        }

        public int Locks { get; set; } = 0;

        public Player(Game game, List<Rectangle> collisionObjects, Dictionary<int, Rectangle> locksObjects)
        {
            _gameRef = (MainGame)game;
            _collisionObjects = collisionObjects;
            _locksObjects = locksObjects;
            _position = new Vector2(64, 96);
            _playerBounds = new Rectangle(64 + 9, 96 + 16, 16, 16);
            _receivedLocksObjects = new Dictionary<int, Rectangle>();
        }

        public void LoadContent()
        {
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

            _pRect = _gameRef.Content.Load<Texture2D>("GUI/ListBoxImage");
        }

        private bool CheckCollisions(AnimationKey key)
        {
            var _playerBoundsTest = _playerBounds;

            if (key == AnimationKey.Up)
            {
                _playerBoundsTest.X = _playerBounds.X;
                _playerBoundsTest.Y = (int)(_playerBounds.Y - 10);
            }

            if (key == AnimationKey.Down)
            {
                _playerBoundsTest.X = _playerBounds.X;
                _playerBoundsTest.Y = (int)(_playerBounds.Y + 10);
            }

            if (key == AnimationKey.Left)
            {
                _playerBoundsTest.X = (int)_playerBounds.X - 10;
                _playerBoundsTest.Y = _playerBounds.Y;
            }

            if (key == AnimationKey.Right)
            {
                _playerBoundsTest.X = (int)_playerBounds.X + 10;
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

        private void GetLocksObjects()
        {
            foreach (var obj in _locksObjects)
            {
                if (obj.Value.Intersects(_playerBounds) == true)
                {
                    if (_receivedLocksObjects.ContainsKey(obj.Key) == false)
                    {
                        _receivedLocksObjects.Add(obj.Key, obj.Value);
                        Locks += 1;
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);

            _positionOld = _position;

            if (InputHandler.KeyDown(Keys.W))
            {
                _sprite.CurrentAnimation = AnimationKey.Up;

                if (CheckCollisions(AnimationKey.Up) == true)
                {
                    _position.Y -= _sprite.Speed;
                }
            }

            if (InputHandler.KeyDown(Keys.S))
            {
                _sprite.CurrentAnimation = AnimationKey.Down;
     
                if (CheckCollisions(AnimationKey.Down) == true)
                {
                    _position.Y += _sprite.Speed;
                }
            }

            if (InputHandler.KeyDown(Keys.A))
            {
                _sprite.CurrentAnimation = AnimationKey.Left;
                
                if (CheckCollisions(AnimationKey.Left) == true)
                {
                    _position.X -= _sprite.Speed;
                }
            }

            if (InputHandler.KeyDown(Keys.D))
            {
                _sprite.CurrentAnimation = AnimationKey.Right;

                if (CheckCollisions(AnimationKey.Right) == true)
                {
                    _position.X += _sprite.Speed;
                }
            }

            GetLocksObjects();

            if (_position != _positionOld)
            {
                _sprite.IsAnimating = true;

                _position.Normalize();

                _sprite.Position += _position * _sprite.Speed;

                _playerBounds.X = (int)_sprite.Position.X + 9;
                _playerBounds.Y = (int)_sprite.Position.Y + 16;
            }
            else
            {
                _sprite.IsAnimating = false;
            }
        }

        public void Draw(GameTime gameTime)
        {
            _sprite.Draw(gameTime, _gameRef.SpriteBatch);
        }
    }
}
