using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;
using static Snake2.Game;

namespace Snake2
{
    class Program
    {
        static void Main() //main, obviously
        {
        restart:
            Clear(); //clear console
            Start(); //start game
            Init.Board(); //initialize board
            Init.Snake(); //initialize snake
            Init.Apple(); //initialize apple
            Move.Body(); //move body, main game loop
            Clear(); //once game is ended, clear console
            HighScore(); //run if high score
            Clear();

            //funny prompts waste lines
            if(death == 1)
            {
                WriteLine("You ran into the wall, OUCH!");
            }

            else if(death == 2)
            {
                WriteLine("You ate yourself, YUM!");
            }

            WriteLine("You scored {0} on {1}\nHigh score is {2} held by {3}\nPress any key to play again. Press ctrl + c to exit."
                , Draw.snakeLength - 1, diffSel, highScore - 1, highScoreStr);

            ReadLine();
            goto restart;
        }
    }
}
