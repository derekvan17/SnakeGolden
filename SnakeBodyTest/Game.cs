using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Snake2.Draw;
using static System.Console;

namespace Snake2
{
    class Game
    {
        public static int speed;
        public static int death = 0;
        public static int highScore = 1;
        public static bool highScoreBool = false;
        public static string highScoreStr = "nobody";
        public static string diffSel;
        public static bool end;

        public static void Start() //difficulty prompt... i want this to return a value to the Main
        {
        restart:
            end = false;
            death = 0;
            WriteLine("Welcome to SSSSSNAKE! Try not to die!\nSelect Difficulty: 1=Hardest 5=Easiest");
            diffSel = ReadLine();
            int difficulty;

            if(int.TryParse(diffSel, out difficulty) && difficulty <= 5 && difficulty >0)
            {
                speed = 30 * difficulty;
            }
            else
            {
                Clear();
                WriteLine("Invalid Difficulty Selection!");
                goto restart;
            }

            switch (difficulty)
            {
                case 1:
                    diffSel = "expert";
                    break;

                case 2:
                    diffSel = "hard";
                    break;

                case 3:
                    diffSel = "medium";
                    break;

                case 4:
                    diffSel = "easy";
                    break;

                case 5:
                    diffSel = "very easy";
                    break;
            }
        }

        public static void EndCheck() //checks for game end... i want this to return a value to Move
        {

            if (snakeX[1] <= 0 || snakeY[1] <= 0 || snakeX[1] == Init.width || snakeY[1] == Init.height) //border crash
            {
                death = 1;
                end = true;
            }

            for (int i = 2; i <= snakeLength; i++) //eaten itself
            {
                if (snakeX[1] == snakeX[i] && snakeY[1] == snakeY[i])
                {
                    death = 2;
                    end = true;
                }
            }

            if(snakeLength > highScore) //check for high score
            {
                highScore = snakeLength;
                highScoreBool = true;
            }

        }

        public static void HighScore() //if high score is acheived, enter name prompt
        {
            if (highScoreBool)
            {
                WriteLine("Congratulations! New High Score of {0}!! Enter your name:", highScore - 1);
                highScoreStr = ReadLine();
                highScoreBool = false;
            }
        }

    }
}
