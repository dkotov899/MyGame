using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MyGame.GameStates;
using MyGame.Input;

namespace MyGame.GameScreens
{
    public class StartMenuScreen : BaseGameState
    {
        public StartMenuScreen(Game game, GameStateManager manager)
        : base(game, manager)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyReleased(Keys.Escape))
            {
                Game.Exit();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

    }
}
