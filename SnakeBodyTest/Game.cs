using System;
using System.Collections.Generic;
using System.Threading;
using Snake2.Enumerations;
using Snake2.Models;

namespace Snake2
{
    public class Game
    {
        public Apple Apple { get; set; }

        public Snake Snake { get; set; }

        public Dictionary<ConsoleKey, Movement> ConsoleKeyMovementMap = new Dictionary<ConsoleKey, Movement>
        {
            { ConsoleKey.RightArrow, Movement.Right },
            { ConsoleKey.LeftArrow, Movement.Left },
            { ConsoleKey.UpArrow, Movement.Up },
            { ConsoleKey.DownArrow, Movement.Down }
        };

        public GameResult? Death { get; set; }

        public string DiffSel { get; set; }

        public int HighScore = 1;

        public int Speed { get; set; }

        public string UserHighScoreName { get; set; }
        
        public Game()
        {
            Start();

            Apple = new Apple();

            Snake = new Snake();
        }
        
        public void Start()
        {
            Console.WriteLine("Welcome to SSSSSNAKE! Try not to die!\nSelect Difficulty: 1=Hardest 5=Easiest");

            int difficulty;

            if (int.TryParse(Console.ReadLine(), out difficulty) && difficulty <= 5 && difficulty > 0)
            {
                Speed = 30 * difficulty;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid Difficulty Selection!");
                Start();
            }

            switch (difficulty)
            {
                case 1:
                    DiffSel = "expert";
                    break;

                case 2:
                    DiffSel = "hard";
                    break;

                case 3:
                    DiffSel = "medium";
                    break;

                case 4:
                    DiffSel = "easy";
                    break;

                case 5:
                    DiffSel = "very easy";
                    break;
            }
            
            InitBoard();
        }

        public void Run()
        {
            while (!Death.HasValue)
            {
                Thread.Sleep(Speed);

                if (EndCheck())
                {
                    break;
                }

                if (Apple.AppleX == Snake.SnakeX[1] && Apple.AppleY == Snake.SnakeY[1])
                {
                    Apple.GotApple = true;
                    Snake.BodyGrow();
                }
                
                Console.Clear();

                Scoreboard();

                Snake.Draw();

                Apple.Draw();

                if (Console.KeyAvailable)
                {
                    Movement nextMovement;
                    
                    if (ConsoleKeyMovementMap.TryGetValue(Console.ReadKey(true).Key, out nextMovement) && nextMovement != Snake.Movement[1])
                    {
                        Snake.Movement[1] = nextMovement;
                    }
                }

                Snake.Update();
            }
        }

        public bool EndCheck()
        {
            Death = Snake.CheckIfSnakeDied();

            return Death.HasValue;
        }

        public void PrintHighScore()
        {
            Console.Clear();

            var snakeLength = Snake.SnakeLength;
            
            if (snakeLength > HighScore)
            {
                HighScore = snakeLength;
                Console.WriteLine("Congratulations! New High Score of {0}!! Enter your name:", HighScore - 1);
                UserHighScoreName = Console.ReadLine();
            }

            if (Death == GameResult.CrashedIntoWall)
            {
                Console.WriteLine("You ran into the wall, OUCH!");
            }
            else if (Death == GameResult.CrashedIntoSelf)
            {
                Console.WriteLine("You ate yourself, YUM!");
            }

            Console.WriteLine("You scored {0} on {1}\nHigh score is {2} held by {3}\nPress any key to play again. Press ctrl + c to exit."
                , snakeLength - 1, DiffSel, HighScore - 1, UserHighScoreName);

            Console.ReadLine();

            Console.Clear();
        }

        public void Scoreboard()
        {
            Console.WriteLine("SSSSSNAKE!  Score: {0}  Difficulty: {1}  High Score: {2}", Snake.SnakeLength - 1, DiffSel, HighScore - 1);
        }

        public void InitBoard()
        {
            if (Console.WindowHeight < 40)
            {
                Console.WindowHeight = 40;
            }
            
            if (Console.WindowWidth < 100)
            {
                Console.WindowWidth = 100;
            }
        }
    }
}
