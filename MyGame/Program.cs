using System;

namespace MyGame
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new MainGame())
            {
                game.Run();
            }
        }
    }
}
