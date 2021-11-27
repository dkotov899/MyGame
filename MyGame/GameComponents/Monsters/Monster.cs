using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.Components.Players
{
    public enum MonsterAction
    {
        WalkUp,
        WalkDown,
        WalkLeft,
        WalkRight
    }

    public class Monster
    {
        private AnimatedSprite _sprite;

        private Vector2 _position;
        private Vector2 _positionOld;

        private Rectangle _monsterBounds;
        private Rectangle _route;

        private MonsterAction[] _monsterActions;
        private MonsterAction _currentAction;

        private Game _gameRef;
        private Player _player;

        public Monster(Game game, Player player, Vector2 startPosition, Rectangle route)
        {
            _gameRef = game;
            _player = player;
            _position = startPosition;
            _route = route;
        }

        public void LoadContent()
        {
            _sprite = new AnimatedSprite(
                _gameRef.Content.Load<Texture2D>("Monsters/malefighter"),
                new Dictionary<AnimationKey, Animation>()
                {
                    { AnimationKey.Down, new Animation(3, 32, 32, 0, 0)  },
                    { AnimationKey.Left, new Animation(3, 32, 32, 0, 32) },
                    { AnimationKey.Right, new Animation(3, 32, 32, 0, 64) },
                    { AnimationKey.Up, new Animation(3, 32, 32, 0, 96) }
                });

            _sprite.Position = _position;

            _monsterActions = new MonsterAction[]
            {
                MonsterAction.WalkUp, MonsterAction.WalkDown,
                MonsterAction.WalkLeft, MonsterAction.WalkRight
            };

            _currentAction = _monsterActions[0];

            _monsterBounds = new Rectangle
                (
                    (int)_position.X + 8, (int)_position.Y + 16,
                    16, 16
                );
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);

            _positionOld = _position;

            switch (_currentAction)
            {
                case MonsterAction.WalkUp:

                    _sprite.CurrentAnimation = AnimationKey.Up;

                    if (CheckRouteCollisions(AnimationKey.Up) == true)
                    {
                        _position.Y -= _sprite.Speed;
                    }

                    break;

                case MonsterAction.WalkDown:

                    _sprite.CurrentAnimation = AnimationKey.Down;

                    if (CheckRouteCollisions(AnimationKey.Down) == true)
                    {
                        _position.Y += _sprite.Speed;
                    }

                    break;

                case MonsterAction.WalkLeft:

                    _sprite.CurrentAnimation = AnimationKey.Left;

                    if (CheckRouteCollisions(AnimationKey.Left) == true)
                    {
                        _position.X -= _sprite.Speed;
                    }

                    break;

                case MonsterAction.WalkRight:

                    _sprite.CurrentAnimation = AnimationKey.Right;

                    if (CheckRouteCollisions(AnimationKey.Right) == true)
                    {
                        _position.X += _sprite.Speed;
                    }

                    break;
            }

            if (_position != _positionOld)
            {
                _sprite.IsAnimating = true;

                _position.Normalize();

                _sprite.Position += _position * _sprite.Speed;

                _monsterBounds.X = (int)_sprite.Position.X + 8;
                _monsterBounds.Y = (int)_sprite.Position.Y + 16;
            }
            else
            {
                _sprite.IsAnimating = false;
            }

            PlayerCollisions();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(gameTime, spriteBatch);
        }

        private bool CheckRouteCollisions(AnimationKey key)
        {
            var monsterBoundsTest = _monsterBounds;

            if (key == AnimationKey.Up)
            {
                monsterBoundsTest.X = _monsterBounds.X;
                monsterBoundsTest.Y = (int)(_monsterBounds.Y - 4);
            }

            if (key == AnimationKey.Down)
            {
                monsterBoundsTest.X = _monsterBounds.X;
                monsterBoundsTest.Y = (int)(_monsterBounds.Y + 4);
            }

            if (key == AnimationKey.Left)
            {
                monsterBoundsTest.X = (int)_monsterBounds.X - 8;
                monsterBoundsTest.Y = _monsterBounds.Y;
            }

            if (key == AnimationKey.Right)
            {
                monsterBoundsTest.X = (int)_monsterBounds.X + 8;
                monsterBoundsTest.Y = _monsterBounds.Y;
            }

            if (_route.Contains(monsterBoundsTest) == false)
            {
                ResetAction();
                return false;
            }

            return true;
        }

        private void PlayerCollisions()
        {
            if (_monsterBounds.Intersects(_player.PlayerBounds) == true)
            {
                _player.IsAlive = false;
            }
        }

        private void ResetAction()
        { 
            _currentAction = _monsterActions[new Random().Next(0, 4)];
        }
    }
}
