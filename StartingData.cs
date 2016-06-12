using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace academy_project
{
    public class StartingData
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Ball Ball { get; set; }
        public Canvas Pitch { get; set; }

        public StartingData(Player player1, Player player2, Ball ball, Canvas pitch)
        {
            Player1 = player1;
            Player2 = player2;
            Ball = ball;
            Pitch = pitch;
        }
    }
}
