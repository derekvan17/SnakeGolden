using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static Snake2.Move;

namespace Snake2
{
    class Draw
    {
        public static int[] snakeX = new int[2499];
        public static int[] snakeY = new int[2499];
        public static int appleX = 0;
        public static int appleY = 0;
        public static int snakeLength = 1;
        public static bool appleGot = false;

        public static void Apple() //draws the apple
        {
            Console.SetCursorPosition(appleX, appleY); //constantly writes apple which x and y only change on appleGot
            Console.Write("■");

            if (appleGot) //randomly decide apple x and y
            {
                Random rand = new Random();
                appleX = rand.Next(1, Init.width-1);
                appleY = rand.Next(1, Init.height-1);
                appleGot = false;
            }
        }

        public static void Scoreboard() //displays score, difficulty, and high score
        {
            Console.WriteLine("SSSSSNAKE!  Score: {0}  Difficulty: {1}  High Score: {2}", snakeLength - 1, Game.diffSel, Game.highScore - 1);
        }

        public static void Snake() //draws the snake
        {
            Console.Clear(); //clears the console so X doesn't show up every scan
            Scoreboard(); //draw scoreboard every scan           

            for (int i = 1; i <= snakeLength; i++) //writes the snakes body
            {
                Console.SetCursorPosition(snakeX[i], snakeY[i]);
                Console.Write("■");
            }
        }
    }
}
