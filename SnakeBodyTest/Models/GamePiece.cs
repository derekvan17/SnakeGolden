using System;

namespace Snake2.Models
{
    public abstract class GamePiece : IGamePiece
    {
        public int Height = 40;

        public int Width = 100;

        public virtual void ApplyRandomPosition() { }

        public virtual void Draw() { }
        
        public void SetCursorAndDraw(int xCoord, int yCoord, bool drawBlock = true)
        {
            Console.SetCursorPosition(xCoord, yCoord);

            if (drawBlock)
            {
                Console.Write("■");
            }
        }

        public Tuple<int, int> GetRandomPosition()
        {
            var random = new Random();

            return new Tuple<int, int>(random.Next(1, Width - 1), random.Next(1, Height - 1));
        }
    }
}
