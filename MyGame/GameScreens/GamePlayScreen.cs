using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Components.Players;
using MyGame.GameStates;
using System.Collections.Generic;

namespace MyGame.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        private Player _player;
        private List<Monster> _monsters;

        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            _player = new Player(game);

            _monsters = new List<Monster>()
            {
                new Monster(game,
                    new Vector2(30, 30),
                    MonsterType.Fighter,
                    new MonsterAction[]
                    { 
                        MonsterAction.WalkRight,
                        MonsterAction.WalkDown,
                        MonsterAction.WalkLeft,
                        MonsterAction.WalkUp,
                    }),

                new Monster(game,
                    new Vector2(30, 30),
                    MonsterType.Wizard,
                    new MonsterAction[]
                    {
                        MonsterAction.WalkDown,
                        MonsterAction.WalkRight,
                        MonsterAction.WalkUp,
                        MonsterAction.WalkLeft,
                    }),
            };
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _player.LoadContent();

            _monsters.ForEach(x => x.LoadContent());

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _monsters.ForEach(x => x.Update(gameTime));
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

            _monsters.ForEach(x => x.Draw(gameTime));

            base.Draw(gameTime);

            GameRef.SpriteBatch.End();
        }
    }
}
