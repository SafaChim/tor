using System;
using OpenTk;

public class Class1
{
    public class Game : GameWindow
    {
        public Game() : base()
        {
        }
    }

    public partial class Main
    {

        public Main()
        {
            using (Game game = new Game())
            {
                game.Run();
            }

        }


    }
}
