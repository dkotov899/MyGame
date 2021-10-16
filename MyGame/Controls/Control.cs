using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Controls
{
    public abstract class Control
    {
        protected string _name;
        protected string _text;
        protected Vector2 _size;
        protected Vector2 _position;
        protected object _value;
        protected bool _hasFocus;
        protected bool _enabled;
        protected bool _visible;
        protected bool _tabStop;
        protected SpriteFont _spriteFont;
        protected Color _color;
        protected string _type;

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
