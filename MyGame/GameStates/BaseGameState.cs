using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Controls;

namespace MyGame.GameStates
{
    public abstract partial class BaseGameState : GameState
    {
        private ContentManager _content;

        protected MainGame GameRef { get; }
        protected ControlManager ControlManager;
        protected PlayerIndex PlayerIndexInControl;

        public BaseGameState(Game game, GameStateManager manager)
            : base(game, manager)
        {
            GameRef = (MainGame)game;
            PlayerIndexInControl = PlayerIndex.One;
        }

        protected override void LoadContent()
        {
            _content = Game.Content;
            var menuFont = _content.Load<SpriteFont>(@"Fonts\ControlFont");
            ControlManager = new ControlManager(menuFont);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
