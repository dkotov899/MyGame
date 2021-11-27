using Microsoft.Xna.Framework;

using MyGame.GameStates;
using MyGame.GameComponents.World;
using MyGame.Input;
using Microsoft.Xna.Framework.Input;

namespace MyGame.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        public GamePlayScreen(Game game, GameStateManager manager)
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

            GameLevelManager.CurrentLevel.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameLevelManager.CurrentLevel.LevelState == LevelState.Active)
            {
                GameLevelManager.CurrentLevel.Update(gameTime);
            }

            if (GameLevelManager.CurrentLevel.LevelState == LevelState.GameOver)
            {
                Transition(ChangeType.Change, _gameRef.GameOverScreen);
                GameLevelManager.ResetLevel();
            }

            if (GameLevelManager.CurrentLevel.LevelState == LevelState.GameWin)
            {
                Transition(ChangeType.Change, _gameRef.GameOverScreen);
                GameLevelManager.ResetLevel();
            }

            if (InputHandler.KeyReleased(Keys.Escape))
            {
                Transition(ChangeType.Change, _gameRef.GameStartMenuScreen);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (GameLevelManager.CurrentLevel.LevelState == LevelState.Active)
                GameLevelManager.CurrentLevel.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}