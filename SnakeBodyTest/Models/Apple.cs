using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2.Models
{
    public class Apple : GamePiece
    {
        public bool GotApple { get; set; }

        public int AppleX { get; set; }

        public int AppleY { get; set; }

        public Apple()
        {
            ApplyRandomPosition();

            SetCursorAndDraw(AppleX, AppleY, false);
        }

        public override void ApplyRandomPosition()
        {
            var startingPosition = GetRandomPosition();

            AppleX = startingPosition.Item1;

            AppleY = startingPosition.Item2;
        }

        public override void Draw()
        {
            SetCursorAndDraw(AppleX, AppleY);

            if (GotApple)
            {
                ApplyRandomPosition();

                GotApple = false;
            }
        }
    }
}
