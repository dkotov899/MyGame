using Microsoft.Xna.Framework;

namespace MyGame.GameStates
{
    public abstract partial class BaseGameState : GameState
    {
        protected MainGame GameRef { get; }

        public BaseGameState(Game game, GameStateManager manager)
            : base(game, manager)
        {
            GameRef = (MainGame)game;
        }
    }
}
