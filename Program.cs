using System;
using System.Threading;

namespace ransanmoi
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Score> scores = new List<Score>();

            // Đường dẫn chứa file lưu điểm
            string filePath = "/Applications/Document/Learning Unity/ransanmoi/Score.json";

            // Load điểm khi bắt đầu chương trình
            scores = ScoreManager.LoadScores(filePath);

            // Giả sử người chơi vừa đạt được điểm mới
            scores.Add(new Score("Player 1", 100));

            // Save điểm khi kết thúc chương trình
            ScoreManager.SaveScores(scores, filePath);

            SnakeControl snakeControl = new SnakeControl();

            Thread gameThread = new Thread(snakeControl.ListenKey);
            gameThread.Start();

            do
            {
                Console.Clear();
                snakeControl.Drawboard();
                snakeControl.setUpBoard();

                if (!snakeControl.isPaused) // Kiểm tra trạng thái tạm dừng
                {
                    snakeControl.MoveSnakeHead();
                    snakeControl.EatFood();
                    snakeControl.SpawnBody();
                    snakeControl.PopUpfood();
                }
                
                snakeControl.ShowPoint();
                Thread.Sleep(snakeControl.speed);
            }
            while (true);
        }
    }
}
