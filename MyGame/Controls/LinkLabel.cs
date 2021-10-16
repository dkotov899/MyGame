using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Input;

namespace MyGame.Controls
{
    public class LinkLabel : Control
    {
        private Color _selectedColor = Color.Red;

        public Color SelectedColor
        {
            get { return _selectedColor; }
            set { _selectedColor = value; }
        }

        public LinkLabel()
        {
            TabStop = true;
            HasFocus = false;
            Position = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (HasFocus)
            {
                spriteBatch.DrawString(SpriteFont, Text, Position, _selectedColor);
            }
            else
            {
                spriteBatch.DrawString(SpriteFont, Text, Position, Color);
            }
        }

        public override void HandleInput(PlayerIndex playerIndex)
        {
            if (!HasFocus)
            {
                return;
            }

            if (InputHandler.KeyReleased(Keys.Enter))
            {
                base.OnSelected(null);
            }

            if (InputHandler.CheckMouseReleased(MouseButton.Left))
            {
                Size = SpriteFont.MeasureString(Text);

                Rectangle r = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)Size.X,
                    (int)Size.Y);

                if (r.Contains(InputHandler.MouseAsPoint))
                {
                    base.OnSelected(null);
                }
            }
        }
    }
}
