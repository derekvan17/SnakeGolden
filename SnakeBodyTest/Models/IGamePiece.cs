using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2.Models
{
    interface IGamePiece
    {
        void ApplyRandomPosition();

        void Draw();
    }
}
