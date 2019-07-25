using Snake2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2.Models
{
    public class Snake : GamePiece
    {
        public Movement[] Movement { get; set; }
        public int SnakeLength { get; set; }
        public int[] SnakeX { get; set; }
        public int[] SnakeY { get; set; }

        public Snake()
        {
            Movement = new Movement[2499];
            Movement[1] = Enumerations.Movement.Right;

            SnakeX = new int[2499];
            SnakeY = new int[2499];

            ApplyRandomPosition();

            SetCursorAndDraw(SnakeX[1], SnakeY[1], false);
        }

        public override void ApplyRandomPosition()
        {
            var start = GetRandomPosition();
            SnakeX[1] = start.Item1;
            SnakeY[1] = start.Item2;
        }

        public void BodyGrow()
        {
            for (int i = 1; i <= SnakeLength; i++)
            {
                if (Movement[i] == Enumerations.Movement.Right)
                {
                    SnakeX[i + 1] = SnakeX[i] - 1;
                    SnakeY[i + 1] = SnakeY[i];
                }
                else if (Movement[i] == Enumerations.Movement.Left)
                {
                    SnakeX[i + 1] = SnakeX[i] + 1;
                    SnakeY[i + 1] = SnakeY[i];
                }
                else if (Movement[i] == Enumerations.Movement.Down)
                {
                    SnakeY[i + 1] = SnakeY[i] - 1;
                    SnakeX[i + 1] = SnakeX[i];
                }
                else if (Movement[i] == Enumerations.Movement.Up)
                {
                    SnakeY[i + 1] = SnakeY[i] + 1;
                    SnakeX[i + 1] = SnakeX[i];
                }
            }

            SnakeLength++;
        }

        public GameResult? CheckIfSnakeDied()
        {
            if (SnakeX[1] <= 0 || SnakeY[1] <= 0 || SnakeX[1] == Width || SnakeY[1] == Height)
            {
                return GameResult.CrashedIntoWall;
            }

            for (int i = 2; i <= SnakeLength; i++)
            {
                if (SnakeX[1] == SnakeX[i] && SnakeY[1] == SnakeY[i])
                {
                    return GameResult.CrashedIntoSelf;
                }
            }

            return null;
        }

        public override void Draw()
        {
            for (int i = SnakeLength; i > 0; i--)
            {
                Movement[i + 1] = Movement[i];
            }

            for (int i = 1; i <= SnakeLength; i++)
            {
                SetCursorAndDraw(SnakeX[i], SnakeY[i]);
            }
        }

        public void Update()
        {
            for (int i = 1; i <= SnakeLength; i++)
            {
                switch (Movement[i])
                {
                    case Enumerations.Movement.Right:
                        SnakeX[i]++;
                        break;

                    case Enumerations.Movement.Left:
                        SnakeX[i]--;
                        break;

                    case Enumerations.Movement.Down:
                        SnakeY[i]++;
                        break;

                    case Enumerations.Movement.Up:
                        SnakeY[i]--;
                        break;
                }
            }
        }
    }
}
