using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace MyGame.GameStates
{
    public enum ChangeType { Change, Pop, Push }

    public class GameStateManager : GameComponent
    {
        private int _drawOrder;
        private Stack<GameState> _gameStates = new Stack<GameState>();

        const int startDrawOrder = 5000;
        const int drawOrderInc = 100;

        public event EventHandler OnStateChange;

        public GameState CurrentState
        {
            get { return _gameStates.Peek(); }
        }

        public GameStateManager(Game game)
        : base(game)
        {
            _drawOrder = startDrawOrder;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void PopState()
        {
            if (_gameStates.Count > 0)
            {
                RemoveState();

                _drawOrder -= drawOrderInc;

                if (OnStateChange != null)
                {
                    OnStateChange(this, null);
                }
            }
        }

        private void RemoveState()
        {
            GameState State = _gameStates.Peek();
            OnStateChange -= State.StateChange;
            Game.Components.Remove(State);
            _gameStates.Pop();
        }

        public void PushState(GameState newState)
        {
            _drawOrder += drawOrderInc;
            newState.DrawOrder = _drawOrder;
            AddState(newState);

            if (OnStateChange != null)
            {
                OnStateChange(this, null);
            }
        }

        private void AddState(GameState newState)
        {
            _gameStates.Push(newState);
            Game.Components.Add(newState);
            OnStateChange += newState.StateChange;
        }

        public void ChangeState(GameState newState)
        {
            while (_gameStates.Count > 0)
            {
                RemoveState();
            }

            newState.DrawOrder = startDrawOrder;
            _drawOrder = startDrawOrder;
            AddState(newState);

            if (OnStateChange != null)
            {
                OnStateChange(this, null);
            }
        }
    }
}
