using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Input;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.Components.Players
{
    public class Player
    {
        private MainGame _gameRef;
        private AnimatedSprite _sprite;

        public int Gold { get; set; }

        public Player(Game game)
        {
            _gameRef = (MainGame)game;
        }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            _sprite = new AnimatedSprite(
                _gameRef.Content.Load<Texture2D>("Player/malerogue"),
                new Dictionary<AnimationKey, Animation>()
                {
                    { AnimationKey.Down, new Animation(3, 32, 32, 0, 0)  },
                    { AnimationKey.Left, new Animation(3, 32, 32, 0, 32) },
                    { AnimationKey.Right, new Animation(3, 32, 32, 0, 64) },
                    { AnimationKey.Up, new Animation(3, 32, 32, 0, 96) }
                });
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);

            var motion = new Vector2();

            if (InputHandler.KeyDown(Keys.W))
            {
                _sprite.CurrentAnimation = AnimationKey.Up;
                motion.Y = -1;
            }

            if (InputHandler.KeyDown(Keys.S))
            {
                _sprite.CurrentAnimation = AnimationKey.Down;
                motion.Y = 1;
            }

            if (InputHandler.KeyDown(Keys.A))
            {
                _sprite.CurrentAnimation = AnimationKey.Left;
                motion.X = -1;
            }

            if (InputHandler.KeyDown(Keys.D))
            {
                _sprite.CurrentAnimation = AnimationKey.Right;
                motion.X = 1;
            }

            if (motion != Vector2.Zero)
            {
                _sprite.IsAnimating = true;

                motion.Normalize();

                _sprite.Position += motion * _sprite.Speed;
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

        public void updateGold(int gold)
        {
            Gold += gold;
        }
    }
}
