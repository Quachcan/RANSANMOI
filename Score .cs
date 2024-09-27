using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace ransanmoi
{
        public class Score
    {
        public string PlayerName { get; set; }
        public int Points { get; set; }

        public Score(string playerName, int points)
        {
            PlayerName = playerName;
            Points = points;
        }
    }
}