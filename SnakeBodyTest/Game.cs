using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Snake2.Enumerations;

namespace Snake2
{
    public class Game
    {
        public int Height = 40;
        public int Width = 100;

        public Movement Movement { get; set; } //each piece of body has a movement direction assoicated with it
        public int[] SnakeX = new int[2499];
        public int[] SnakeY = new int[2499];

        public int HighScore { get; set; }
        public string UserHighScoreName { get; set; }

        public bool AppleGot = false;
        public int AppleX = 0;
        public int AppleY = 0;
        public int Death = 0;
        public string DiffSel;
        public bool GameHasEnded;
        public int SnakeLength = 1;
        public int Speed;

        public Game()
        {
            Start();
            InitBoard();
            InitSnake();
            InitApple();
        }
        
        public void Start() //difficulty prompt... i want this to return a value to the Main
        {
            GameHasEnded = false;
            Death = 0;
            Console.WriteLine("Welcome to SSSSSNAKE! Try not to die!\nSelect Difficulty: 1=Hardest 5=Easiest");
            DiffSel = Console.ReadLine();
            int difficulty;

            if (int.TryParse(DiffSel, out difficulty) && difficulty <= 5 && difficulty > 0)
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
        }

        public void Body() //main loop of program, moves body
        {
            Movement = Movement.Right;  //always start going in the right direction
            while (!GameHasEnded) //main loop
            {
                Thread.Sleep(Speed); //refreshes at difficulty speed
                EndCheck(); //ends game at any end conditions

                if (AppleX == SnakeX[1] && AppleY == SnakeY[1]) //check if the apple was got
                {
                    AppleGot = true;
                    BodyGrow();
                }

                //for (int i = SnakeLength; i > 0; i--) //index movement array
                //{
                //    Movement[i + 1] = Movement[i];
                //}

                Snake(); //draws snake every scan
                Apple(); //draws apple every scan

                if (Console.KeyAvailable) //key input
                {
                    var turn = Console.ReadKey(true).Key;

                    if (turn.Equals(ConsoleKey.RightArrow) && Movement != Movement.Right)
                    {
                        Movement = Movement.Right;
                    }

                    if (turn.Equals(ConsoleKey.LeftArrow) && Movement != Movement.Left)
                    {
                        Movement = Movement.Left;
                    }

                    if (turn.Equals(ConsoleKey.UpArrow) && Movement != Movement.Up)
                    {
                        Movement = Movement.Up;
                    }

                    if (turn.Equals(ConsoleKey.DownArrow) && Movement != Movement.Down)
                    {
                        Movement = Movement.Down;
                    }
                }

                for (int i = 1; i <= SnakeLength; i++) //changes direction of snake based on key input
                {
                    switch (Movement)
                    {
                        case Movement.Right:
                            SnakeX[i]++;
                            break;

                        case Movement.Left:
                            SnakeX[i]--;
                            break;

                        case Movement.Down:
                            SnakeY[i]++;
                            break;

                        case Movement.Up:
                            SnakeY[i]--;
                            break;
                    }
                }
            }
        }

        public void EndCheck() //checks for game end... i want this to return a value to Move
        {
            if (SnakeX[1] <= 0 || SnakeY[1] <= 0 || SnakeX[1] == Width || SnakeY[1] == Height) //border crash
            {
                Death = 1;
                GameHasEnded = true;
            }

            for (int i = 2; i <= SnakeLength; i++) //eaten itself
            {
                if (SnakeX[1] == SnakeX[i] && SnakeY[1] == SnakeY[i])
                {
                    Death = 2;
                    GameHasEnded = true;
                }
            }
        }

        public void PrintHighScore() //if high score is acheived, enter name prompt
        {
            Console.Clear();
            
            if (SnakeLength > HighScore) //check for high score
            {
                Console.WriteLine("Congratulations! New High Score of {0}!! Enter your name:", HighScore - 1);
                UserHighScoreName = Console.ReadLine();
            }

            if (Death == 1)
            {
                Console.WriteLine("You ran into the wall, OUCH!");
            }
            else if (Death == 2)
            {
                Console.WriteLine("You ate yourself, YUM!");
            }

            Console.WriteLine("You scored {0} on {1}\nHigh score is {2} held by {3}\nPress any key to play again. Press ctrl + c to exit."
                , SnakeLength - 1, DiffSel, HighScore - 1, UserHighScoreName);

            Console.ReadLine();

            Console.Clear();
        }

        public void Apple() //draws the apple
        {
            Console.SetCursorPosition(AppleX, AppleY); //constantly writes apple which x and y only change on appleGot
            Console.Write("■");

            if (AppleGot) //randomly decide apple x and y
            {
                Random rand = new Random();
                AppleX = rand.Next(1, Width - 1);
                AppleY = rand.Next(1, Height - 1);
                AppleGot = false;
            }
        }

        public void Scoreboard() //displays score, difficulty, and high score
        {
            Console.WriteLine("SSSSSNAKE!  Score: {0}  Difficulty: {1}  High Score: {2}", SnakeLength - 1, DiffSel, HighScore - 1);
        }

        public void Snake() //draws the snake
        {
            Console.Clear(); //clears the console so X doesn't show up every scan
            Scoreboard(); //draw scoreboard every scan           

            for (int i = 1; i <= SnakeLength; i++) //writes the snakes body
            {
                Console.SetCursorPosition(SnakeX[i], SnakeY[i]);
                Console.Write("■");
            }
        }

        public void InitBoard()
        {
            Console.WindowHeight = Height;
            Console.WindowWidth = Height;
        }

        public void InitSnake() 
        {
            Random start = new Random();
            SnakeX[1] = start.Next(1, Width - 1);
            SnakeY[1] = start.Next(1, Height - 1);
            Console.SetCursorPosition(SnakeX[1], SnakeY[1]);
            SnakeLength = 1;
        }

        public void InitApple() //draws the initial apple
        {
            while (true)
            {
                Random rand = new Random();
                AppleX = rand.Next(1, Width - 1);
                AppleY = rand.Next(1, Height - 1);
                Console.SetCursorPosition(AppleX, AppleY);

                if (SnakeX[1] != AppleX || SnakeY[1] != AppleY) //makes sure the apple and snake aren't at the same spot
                {
                    Console.Write("O");
                    break;
                }
                else
                {
                    AppleX = rand.Next(1, 74);
                    AppleY = rand.Next(1, 49);
                }
            }
        }

        public void BodyGrow() //this runs when the body grows
        {
            for (int i = 1; i <= SnakeLength; i++) //if a piece is got, the next piece's position is incremented here
            {
                if (Movement == Movement.Right)
                {
                    SnakeX[i + 1] = SnakeX[i] - 1;
                    SnakeY[i + 1] = SnakeY[i];
                }
                else if (Movement == Movement.Left)
                {
                    SnakeX[i + 1] = SnakeX[i] + 1;
                    SnakeY[i + 1] = SnakeY[i];
                }
                else if (Movement == Movement.Down)
                {
                    SnakeY[i + 1] = SnakeY[i] - 1;
                    SnakeX[i + 1] = SnakeX[i];
                }
                else if (Movement == Movement.Up)
                {
                    SnakeY[i + 1] = SnakeY[i] + 1;
                    SnakeX[i + 1] = SnakeX[i];
                }
            }

            SnakeLength++; //incremement snake length after body coordiantes are updated
        }
    }
}
