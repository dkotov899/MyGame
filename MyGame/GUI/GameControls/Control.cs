using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Controls
{
    public abstract class Control
    {
        private string _name;
        private string _text;
        private Vector2 _size;
        private Vector2 _position;
        private object _value;
        private bool _hasFocus;
        private bool _enabled;
        private bool _visible;
        private bool _tabStop;
        private SpriteFont _spriteFont;
        private Color _color;
        private string _type;

        public event EventHandler Selected;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _position.Y = (int)_position.Y;
            }
        }

        public object Value
        {
            get { return _value; }
            set { this._value = value; }
        }

        public bool HasFocus
        {
            get { return _hasFocus; }
            set { _hasFocus = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public bool TabStop
        {
            get { return _tabStop; }
            set { _tabStop = value; }
        }

        public SpriteFont SpriteFont
        {
            get { return _spriteFont; }
            set { _spriteFont = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Control()
        {
            Color = Color.White;
            Enabled = true;
            Visible = true;
            SpriteFont = ControlManager.SpriteFont;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void HandleInput(PlayerIndex playerIndex);

        protected virtual void OnSelected(EventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, e);
            }
        }
    }
}
