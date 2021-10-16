using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace MyGame.GameStates
{
    public abstract partial class GameState : DrawableGameComponent
    {
        private GameState _tag;
        private List<GameComponent> _childComponents;

        protected GameStateManager StateManager;

        public List<GameComponent> Components
        {
            get { return _childComponents; }
        }

        public GameState Tag
        {
            get { return _tag; }
        }

        public GameState(Game game, GameStateManager manager)
        : base(game)
        {
            StateManager = manager;
            _childComponents = new List<GameComponent>();
            _tag = this;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in _childComponents)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;

            foreach (GameComponent component in _childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;

                    if (drawComponent.Visible)
                    {
                        drawComponent.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }

        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (StateManager.CurrentState == Tag)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;

            foreach (GameComponent component in _childComponents)
            {
                component.Enabled = true;

                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }
        }

        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;

            foreach (GameComponent component in _childComponents)
            {
                component.Enabled = false;

                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = false;
                }
            }
        }
    }
}
