using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame.Input
{
    public class InputHandler : GameComponent
    {
        private static KeyboardState _keyboardState;
        private static KeyboardState _lastKeyboardState;

        public static KeyboardState KeyboardState
        {
            get { return _keyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return _lastKeyboardState; }
        }

        public InputHandler(Game game)
        : base(game)
        {
            _keyboardState = Keyboard.GetState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        public static void Flush()
        {
            _lastKeyboardState = _keyboardState;
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
    }
}
