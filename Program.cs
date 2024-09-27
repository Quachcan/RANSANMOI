using System;
using System.Threading;

namespace ransanmoi
{
    public class Program
    {
        static void Main(string[] args)
        {
            
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
                    snakeControl.GameOver();
                }

                snakeControl.ShowPoint();
                Thread.Sleep(snakeControl.speed);
            }
            while (true);
        }
    }
}
