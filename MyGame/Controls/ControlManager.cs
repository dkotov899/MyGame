using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MyGame.Input;

namespace MyGame.Controls
{
    public class ControlManager : List<Control>
    {
        private int _selectedControl = 0;
        private bool _acceptInput = true;
        private static SpriteFont _spriteFont;

        public event EventHandler FocusChanged;

        public static SpriteFont SpriteFont
        {
            get { return _spriteFont; }
        }

        public bool AcceptInput
        {
            get { return _acceptInput; }
            set { _acceptInput = value; }
        }

        public ControlManager(SpriteFont spriteFont)
            : base()
        {
            ControlManager._spriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, int capacity)
            : base(capacity)
        {
            ControlManager._spriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection)
            : base(collection)
        {
            ControlManager._spriteFont = spriteFont;
        }

        public void Update(GameTime gameTime, PlayerIndex playerIndex)
        {
            if (Count == 0)
            {
                return;
            }

            foreach (Control c in this)
            {
                if (c.Enabled)
                {
                    c.Update(gameTime);
                }

                if (c.HasFocus)
                {
                    c.HandleInput(playerIndex);
                }
            }

            if (!AcceptInput)
            {
                return;
            }

            if (InputHandler.ButtonPressed(Buttons.LeftThumbstickUp, playerIndex) ||
                InputHandler.ButtonPressed(Buttons.DPadUp, playerIndex) ||
                InputHandler.KeyPressed(Keys.Up))
            {
                PreviousControl();
            }

            if (InputHandler.ButtonPressed(Buttons.LeftThumbstickDown, playerIndex) ||
                InputHandler.ButtonPressed(Buttons.DPadDown, playerIndex) ||
                InputHandler.KeyPressed(Keys.Down))
            {
                NextControl();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in this)
            {
                if (c.Visible)
                {
                    c.Draw(spriteBatch);
                }
            }
        }

        public void NextControl()
        {
            if (Count == 0)
            {
                return;
            }

            int currentControl = _selectedControl;
            this[_selectedControl].HasFocus = false;

            do
            {
                _selectedControl++;

                if (_selectedControl == Count)
                {
                    _selectedControl = 0;
                }

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled)
                {
                    if (FocusChanged != null)
                    {
                        FocusChanged(this[_selectedControl], null);
                    }

                    break;
                }
            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }

        public void PreviousControl()
        {
            if (Count == 0)
            {
                return;
            }

            int currentControl = _selectedControl;
            this[_selectedControl].HasFocus = false;

            do
            {
                _selectedControl--;

                if (_selectedControl < 0)
                {
                    _selectedControl = Count - 1;
                }

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled)
                {
                    if (FocusChanged != null)
                    {
                        FocusChanged(this[_selectedControl], null);
                    }

                    break;
                }
            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }
    }
}
