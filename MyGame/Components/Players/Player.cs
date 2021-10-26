using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Input;
using MyGame.Sprites;
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

        public Rectangle PlayerBounds
        {
            get { return _playerBounds; }
        }

        public int Gold { get; set; }

        public Player(Game game, List<Rectangle> collisionObjects)
        {
            _gameRef = (MainGame)game;
            _collisionObjects = collisionObjects;
            _position = new Vector2(64, 96);
            _playerBounds = new Rectangle(64, 96, 32, 32);
        }

        public void Initialize()
        {

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
        }

        private bool CheckCollisions()
        {
            foreach (var rect in _collisionObjects)
            {
                if (rect.Intersects(_playerBounds) == true)
                {
                    _position = _positionOld;

                    return false;
                }
            }

            return true;
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);

            _positionOld = _position;

            if (InputHandler.KeyDown(Keys.W))
            {
                if (CheckCollisions() == true)
                {
                    _sprite.CurrentAnimation = AnimationKey.Up;
                    _position.Y -= _sprite.Speed;
                }              
            }

            if (InputHandler.KeyDown(Keys.S))
            {
                if (CheckCollisions() == true)
                {
                    _sprite.CurrentAnimation = AnimationKey.Down;
                    _position.Y += _sprite.Speed;
                }
            }

            if (InputHandler.KeyDown(Keys.A))
            {
                if (CheckCollisions() == true)
                {
                    _sprite.CurrentAnimation = AnimationKey.Left;
                    _position.X -= _sprite.Speed;
                }
            }

            if (InputHandler.KeyDown(Keys.D))
            {
                if (CheckCollisions() == true)
                {
                    _sprite.CurrentAnimation = AnimationKey.Right;
                    _position.X += _sprite.Speed;
                }
            }

            if (_position != _positionOld)
            {
                _sprite.IsAnimating = true;

                _position.Normalize();

                _sprite.Position += _position * _sprite.Speed;

                _playerBounds.X = (int)_sprite.Position.X;
                _playerBounds.Y = (int)_sprite.Position.Y;
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

        public void updateGold(int gold)
        {
            Gold += gold;
        }
    }
}
