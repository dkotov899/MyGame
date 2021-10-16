using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame.Input
{
    public enum MouseButton { Left, Middle, Right }

    public class InputHandler : GameComponent
    {
        private static KeyboardState _keyboardState;
        private static KeyboardState _lastKeyboardState;

        private static MouseState mouseState;
        private static MouseState lastMouseState;

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
            get { return mouseState; }
        }

        public static MouseState LastMouseState
        {
            get { return lastMouseState; }
        }

        public InputHandler(Game game)
        : base(game)
        {
            _keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        public static void Flush()
        {
            _lastKeyboardState = _keyboardState;
            lastMouseState = mouseState;
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
            get { return new Point(mouseState.X, mouseState.Y); }
        }

        public static Vector2 MouseAsVector2
        {
            get { return new Vector2(mouseState.X, mouseState.Y); }
        }

        public static Point LastMouseAsPoint
        {
            get { return new Point(lastMouseState.X, lastMouseState.Y); }
        }

        public static Vector2 LastMouseAsVector2
        {
            get { return new Vector2(lastMouseState.X, lastMouseState.Y); }
        }

        public static bool CheckMousePress(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:

                    result = mouseState.LeftButton == ButtonState.Pressed &&
                        lastMouseState.LeftButton == ButtonState.Released;

                    break;

                case MouseButton.Right:

                    result = mouseState.RightButton == ButtonState.Pressed &&
                        lastMouseState.RightButton == ButtonState.Released;

                    break;

                case MouseButton.Middle:

                    result = mouseState.MiddleButton == ButtonState.Pressed &&
                        lastMouseState.MiddleButton == ButtonState.Released;

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

                    result = mouseState.LeftButton == ButtonState.Released &&
                        lastMouseState.LeftButton == ButtonState.Pressed;

                    break;

                case MouseButton.Right:

                    result = mouseState.RightButton == ButtonState.Released &&
                        lastMouseState.RightButton == ButtonState.Pressed;

                    break;

                case MouseButton.Middle:

                    result = mouseState.MiddleButton == ButtonState.Released &&
                        lastMouseState.MiddleButton == ButtonState.Pressed;

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

                    result = mouseState.LeftButton == ButtonState.Pressed;

                    break;

                case MouseButton.Right:

                    result = mouseState.RightButton == ButtonState.Pressed;

                    break;

                case MouseButton.Middle:

                    result = mouseState.MiddleButton == ButtonState.Pressed;

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

                    result = mouseState.LeftButton == ButtonState.Released;

                    break;

                case MouseButton.Right:

                    result = mouseState.RightButton == ButtonState.Released;

                    break;

                case MouseButton.Middle:

                    result = mouseState.MiddleButton == ButtonState.Released;

                    break;
            }

            return result;
        }
    }
}
