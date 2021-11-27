using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyGame.Controls;

namespace MyGame.GameStates
{
    public abstract partial class BaseGameState : GameState
    {
        protected MainGame _gameRef;
        protected ControlManager _controlManager;
        protected PlayerIndex _playerIndexInControl;
        protected BaseGameState _transitionTo;
        protected bool _transitioning;
        protected ChangeType _changeType;
        protected TimeSpan _transitionTimer;
        protected TimeSpan _transitionInterval = TimeSpan.FromSeconds(0.1);

        public BaseGameState(Game game, GameStateManager manager)
            : base(game, manager)
        {
            _gameRef = (MainGame)game;
            _playerIndexInControl = PlayerIndex.One;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            var content = Game.Content;
            var menuFont = content.Load<SpriteFont>(@"Fonts\ControlFont");

            _controlManager = new ControlManager(menuFont);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (_transitioning)
            {
                _transitionTimer += gameTime.ElapsedGameTime;

                if (_transitionTimer >= _transitionInterval)
                {
                    _transitioning = false;

                    switch (_changeType)
                    {
                        case ChangeType.Change:

                            StateManager.ChangeState(_transitionTo);

                            break;

                        case ChangeType.Pop:

                            StateManager.PopState();

                            break;

                        case ChangeType.Push:

                            StateManager.PushState(_transitionTo);

                            break;
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public virtual void Transition(ChangeType change, BaseGameState gameState)
        {
            _transitioning = true;
            _changeType = change;
            _transitionTo = gameState;
            _transitionTimer = TimeSpan.Zero;
        }
    }
}
