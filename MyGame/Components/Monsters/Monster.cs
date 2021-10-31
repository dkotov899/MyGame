using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.Components.Players
{
    public enum MonsterType 
    {
        Fighter,
        Wizard
    }

    public enum MonsterAction
    {
        WalkUp,
        WalkDown,
        WalkLeft,
        WalkRight
    }

    public class Monster
    {
        private MainGame _gameRef;
        private AnimatedSprite _sprite;
        private MonsterType _monsterType;
        private MonsterAction[] _monsterActions;
        private MonsterAction _currentAction;

        private Rectangle _monsterBounds;

        private int _indexCurrentAction;

        private int _steps = 60;
        private int _currentStep = 0;

        private Vector2 _position;
        private Vector2 _positionOld;

        private Player _player;

        private bool _isCaugth;

        public bool IsCaught
        {
            get { return _isCaugth; }
            private set { _isCaugth = value; }
        }

        public Monster(Game game, Player player, Vector2 position, int steps, MonsterType monsterType, MonsterAction[] monsterActions)
        {
            _gameRef = (MainGame)game;
            _player = player;
            _position = position;
            _steps = steps;
            _monsterType = monsterType;
            _monsterActions = monsterActions;
            _currentAction = monsterActions[0];
            _indexCurrentAction = 0;
            _monsterBounds = new Rectangle((int)_position.X, (int)_position.Y, 16, 16);
        }

        public void LoadContent()
        {
            var spriteSheet = new object();

            switch (_monsterType)
            {
                case MonsterType.Fighter:

                    spriteSheet = _gameRef.Content.Load<Texture2D>("Monsters/malefighter");

                    break;

                case MonsterType.Wizard:

                    spriteSheet = _gameRef.Content.Load<Texture2D>("Monsters/malewizard");

                    break;
            }

            _sprite = new AnimatedSprite((Texture2D)spriteSheet,
                new Dictionary<AnimationKey, Animation>()
                {
                    { AnimationKey.Down, new Animation(3, 32, 32, 0, 0)  },
                    { AnimationKey.Left, new Animation(3, 32, 32, 0, 32) },
                    { AnimationKey.Right, new Animation(3, 32, 32, 0, 64) },
                    { AnimationKey.Up, new Animation(3, 32, 32, 0, 96) }
                });


            _sprite.Position = _position;
        }

        private void CheckPlayerCollision()
        {
            var playerRect = _player.PlayerBounds;

            if (_monsterBounds.Intersects(playerRect) == true)
            {
                _isCaugth = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);

            var motion = new Vector2();

            _positionOld = _position;

            if (_currentStep == _steps)
            {
                _currentStep = 0;

                if (_indexCurrentAction == _monsterActions.Length - 1)
                {
                    _indexCurrentAction = 0;
                }
                else
                {
                    _indexCurrentAction++;
                }

                _currentAction = _monsterActions[_indexCurrentAction];
            }

            switch(_currentAction)
            {
                case MonsterAction.WalkUp:

                    motion.Y -= 1;
                    _sprite.CurrentAnimation = AnimationKey.Up;
                    _position.Y -= motion.Y;
                    _currentStep += 1;

                    break;

                case MonsterAction.WalkDown:

                    motion.Y += 1;
                    _sprite.CurrentAnimation = AnimationKey.Down;
                    _position.Y += motion.Y;
                    _currentStep += 1;

                    break;

                case MonsterAction.WalkLeft:

                    motion.X -= 1;
                    _sprite.CurrentAnimation = AnimationKey.Left;
                    _position.X -= motion.X;
                    _currentStep += 1;

                    break;

                case MonsterAction.WalkRight:

                    motion.X += 1;
                    _sprite.CurrentAnimation = AnimationKey.Right;
                    _position.X += motion.X;
                    _currentStep += 1;

                    break;
            }

            CheckPlayerCollision();

            if (_position != _positionOld)
            {
                _sprite.IsAnimating = true;

                motion.Normalize();

                _sprite.Position += motion * (_sprite.Speed - 0.4f);


                _monsterBounds.X = (int)_sprite.Position.X + 9;
                _monsterBounds.Y = (int)_sprite.Position.Y + 16;
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
