using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Components.Players;
using MyGame.GameStates;
using MyGame.Input;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        private Player _player;
        private AnimatedSprite _sprite;

        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            _player = new Player(game);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _player.LoadContent();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                null);

            _player.Draw(gameTime);

            base.Draw(gameTime);

            GameRef.SpriteBatch.End();
        }
    }
}
