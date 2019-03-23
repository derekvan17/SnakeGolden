using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static Snake2.Draw;

namespace Snake2
{
    class Move
    {
        public static int[] movement = new int[2499]; //each piece of body has a movement direction assoicated with it

        public static void BodyGrow() //this runs when the body grows
        {
            for (int i = 1; i <= snakeLength; i++) //if a piece is got, the next piece's position is incremented here
            {
                if (movement[i] == 0)
                {
                    snakeX[i + 1] = snakeX[i] - 1;
                    snakeY[i + 1] = snakeY[i];
                }
                else if (movement[i] == 1)
                {
                    snakeX[i + 1] = snakeX[i] + 1;
                    snakeY[i + 1] = snakeY[i];
                }
                else if (movement[i] == 2)
                {
                    snakeY[i + 1] = snakeY[i] - 1;
                    snakeX[i + 1] = snakeX[i];
                }
                else if (movement[i] == 3)
                {
                    snakeY[i + 1] = snakeY[i] + 1;
                    snakeX[i + 1] = snakeX[i];
                }
            }

            snakeLength++; //incremement snake length after body coordiantes are updated
        }

        public static void Body() //main loop of program, moves body
        {
            movement[1] = 0;  //always start going in the right direction
            while (!Game.end) //main loop
            {
                Thread.Sleep(Game.speed); //refreshes at difficulty speed
                Game.EndCheck(); //ends game at any end conditions

                if (appleX == snakeX[1] && appleY == snakeY[1]) //check if the apple was got
                {
                    appleGot = true;
                    BodyGrow();
                }
                
                for (int i = snakeLength; i > 0; i--) //index movement array
                {
                    movement[i + 1] = movement[i];
                }

                Snake(); //draws snake every scan
                Apple(); //draws apple every scan

                if (Console.KeyAvailable) //key input
                {
                    while (true)
                    {
                        ConsoleKeyInfo turn = Console.ReadKey(true);
                        if (turn.Key.Equals(ConsoleKey.RightArrow) && movement[1] != 1)
                        {
                            movement[1] = 0;
                        }

                        if (turn.Key.Equals(ConsoleKey.LeftArrow) && movement[1] != 0)
                        {
                            movement[1] = 1;
                        }

                        if (turn.Key.Equals(ConsoleKey.UpArrow) && movement[1] != 2)
                        {
                            movement[1] = 3;
                        }

                        if (turn.Key.Equals(ConsoleKey.DownArrow) && movement[1] != 3)
                        {
                            movement[1] = 2;
                        }
                        break;
                    }
                }

                for (int i = 1; i<=snakeLength; i++) //changes direction of snake based on key input
                {
                    switch (movement[i])
                    {
                        case 0:
                            snakeX[i]++;
                            break;

                        case 1:
                            snakeX[i]--;
                            break;

                        case 2:
                            snakeY[i]++;
                            break;

                        case 3:
                            snakeY[i]--;
                            break;
                    }
                }
            }
        }
    }
}
