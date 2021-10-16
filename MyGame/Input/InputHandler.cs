﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame.Input
{
    public enum MouseButton { Left, Middle, Right }

    public class InputHandler : GameComponent
    {
        private static KeyboardState _keyboardState;
        private static KeyboardState _lastKeyboardState;

        private static MouseState _mouseState;
        private static MouseState _lastMouseState;

        public static KeyboardState KeyboardState
        {
            get { return _keyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return _lastKeyboardState; }
        }

        public static MouseState MouseState
        {
            get { return _mouseState; }
        }

        public static MouseState LastMouseState
        {
            get { return _lastMouseState; }
        }

        public InputHandler(Game game)
        : base(game)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            _lastMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        public static void Flush()
        {
            _lastKeyboardState = _keyboardState;
            _lastMouseState = _mouseState;
        }

        public static bool KeyReleased(Keys key)
        {
            return _keyboardState.IsKeyUp(key) &&
            _lastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key) &&
            _lastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }

        public static Point MouseAsPoint
        {
            get { return new Point(_mouseState.X, _mouseState.Y); }
        }

        public static Vector2 MouseAsVector2
        {
            get { return new Vector2(_mouseState.X, _mouseState.Y); }
        }

        public static Point LastMouseAsPoint
        {
            get { return new Point(_lastMouseState.X, _lastMouseState.Y); }
        }

        public static Vector2 LastMouseAsVector2
        {
            get { return new Vector2(_lastMouseState.X, _lastMouseState.Y); }
        }

        public static bool CheckMousePress(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:

                    result = _mouseState.LeftButton == ButtonState.Pressed &&
                        _lastMouseState.LeftButton == ButtonState.Released;

                    break;

                case MouseButton.Right:

                    result = _mouseState.RightButton == ButtonState.Pressed &&
                        _lastMouseState.RightButton == ButtonState.Released;

                    break;

                case MouseButton.Middle:

                    result = _mouseState.MiddleButton == ButtonState.Pressed &&
                        _lastMouseState.MiddleButton == ButtonState.Released;

                    break;
            }

            return result;
        }

        public static bool CheckMouseReleased(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:

                    result = _mouseState.LeftButton == ButtonState.Released &&
                        _lastMouseState.LeftButton == ButtonState.Pressed;

                    break;

                case MouseButton.Right:

                    result = _mouseState.RightButton == ButtonState.Released &&
                        _lastMouseState.RightButton == ButtonState.Pressed;

                    break;

                case MouseButton.Middle:

                    result = _mouseState.MiddleButton == ButtonState.Released &&
                        _lastMouseState.MiddleButton == ButtonState.Pressed;

                    break;
            }

            return result;
        }

        public static bool IsMouseDown(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:

                    result = _mouseState.LeftButton == ButtonState.Pressed;

                    break;

                case MouseButton.Right:

                    result = _mouseState.RightButton == ButtonState.Pressed;

                    break;

                case MouseButton.Middle:

                    result = _mouseState.MiddleButton == ButtonState.Pressed;

                    break;
            }

            return result;
        }

        public static bool IsMouseUp(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:

                    result = _mouseState.LeftButton == ButtonState.Released;

                    break;

                case MouseButton.Right:

                    result = _mouseState.RightButton == ButtonState.Released;

                    break;

                case MouseButton.Middle:

                    result = _mouseState.MiddleButton == ButtonState.Released;

                    break;
            }

            return result;
        }
    }
}
