using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MyGame.Input;

namespace MyGame.Controls
{
    public class MenuButton : Control
    {
        private Texture2D _image;
        private Rectangle _destRect;
        private float _opasity;

        public Texture2D Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public Rectangle DestinationRectangle
        {
            get { return _destRect; }
            set { _destRect = value; }
        }

        public float Opasity
        {
            get { return _opasity; }
            set { _opasity = value; }
        }

        public MenuButton(Texture2D image, Rectangle destination)
        {
            _image = image;
            _destRect = destination;
            _opasity = 0.5f;
        }

        public MenuButton(Texture2D image, Rectangle destination, float opasity )
        {
            _image = image;
            _destRect = destination;
            _opasity = opasity;
        }


        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_image, 
                Position,
                _destRect,
                Color.Black * _opasity,
                0,
                new Vector2(0, 0),
                0.5f,
                SpriteEffects.None,
                0f);

            spriteBatch.DrawString(SpriteFont, Text, Position, Color.White);
        }

        public override void HandleInput(PlayerIndex playerIndex)
        {
        }
    }
}
