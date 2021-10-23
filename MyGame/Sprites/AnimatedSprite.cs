using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MyGame.Sprites
{
    public class AnimatedSprite
    {
        private Dictionary<AnimationKey, Animation> _animations;
        private AnimationKey _currentAnimation;
        private bool _isAnimating;
        private Texture2D _texture;
        private Vector2 _velocity;
        private float _speed = 2.0f;

        public Vector2 Position;

        public AnimationKey CurrentAnimation
        {
            get { return _currentAnimation; }
            set { _currentAnimation = value; }
        }

        public bool IsAnimating
        {
            get { return _isAnimating; }
            set { _isAnimating = value; }
        }

        public int Width
        {
            get { return _animations[_currentAnimation].FrameWidth; }
        }

        public int Height
        {
            get { return _animations[_currentAnimation].FrameHeight; }
        }

        public float Speed
        {

            get { return _speed; }
            set { _speed = MathHelper.Clamp(_speed, 1.0f, 16.0f); }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;

                if (_velocity != Vector2.Zero)
                {
                    _velocity.Normalize();
                }
            }
        }

        public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation)
        {
            _texture = sprite;
            _animations = new Dictionary<AnimationKey, Animation>();

            foreach (AnimationKey key in animation.Keys)
            {
                _animations.Add(key, (Animation)animation[key].Clone());
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_isAnimating)
            {
                _animations[_currentAnimation].Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                Position,
                _animations[_currentAnimation].CurrentFrameRect,
                Color.White);
        }
    }
}
