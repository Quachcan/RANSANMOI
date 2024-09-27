using System;
using System.Threading;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace ransanmoi
{
    public class SnakeControl
    {
        public Point food = new Point(8, 8);
        public bool foodExist = false;
        public int speed = 500;
        public int row = 20;
        public int col = 40;
        public string direction = "Right";
        public int score;
        public Point[] body;
        public Point _head;
        public string[,] board;
        public bool isPaused;


        // Constructor để khởi tạo các đối tượng
        public SnakeControl()
        {
            body = new Point[1] { new Point(4, 4) };
            _head = new Point(10, 10);
            board = new string[row, col];
            Drawboard(); // Khởi tạo bàn chơi
        }

        // Vẽ các đối tượng trên bàn (biên, rắn, mồi)
        public void Drawboard()
        {
            try
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (i == 0 || i == row - 1 || j == 0 || j == col - 1)
                        {
                            board[i, j] = "#"; // Biên
                        }
                        else if (i == _head.X && j == _head.Y)
                        {
                            board[i, j] = "■"; // Đầu rắn
                        }
                        else
                        {
                            bool isBodyPart = false;
                            for (int count = 0; count < body.Length; count++)
                            {
                                if (i == body[count].X && j == body[count].Y)
                                {
                                    board[i, j] = "■"; // Thân rắn
                                    isBodyPart = true;
                                    break;
                                }
                            }
                            if (!isBodyPart)
                            {
                                if (i == food.X && j == food.Y)
                                {
                                    board[i, j] = "@"; // Thực phẩm
                                }
                                else
                                {
                                    board[i, j] = " "; // Ô trống
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Drawboard: {ex.Message}");
            }
        }

        // Kiểm tra va chạm với các cạnh của bàn chơi
        public void MoveSnakeHead()
        {
            try
            {
                switch (direction)
                {
                    case "Right":
                        _head.Y += 1;
                        if (_head.Y == col - 1) _head.Y = 1;
                        break;
                    case "Left":
                        _head.Y -= 1;
                        if (_head.Y == 0) _head.Y = col - 1;
                        break;
                    case "Up":
                        _head.X -= 1;
                        if (_head.X == 0) _head.X = row - 1;
                        break;
                    case "Down":
                        _head.X += 1;
                        if (_head.X == row - 1) _head.X = 1;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MoveSnakeHead: {ex.Message}");
            }
        }

        // Đọc vào phím lên, xuống, trái, phải
        public void ListenKey()
        {
            try
            {
                while (true)
                {
                    ConsoleKeyInfo keyinfo = Console.ReadKey(true); 
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (direction == "Up" || direction == "Down" || direction == "Left")
                            {
                                direction = "Right";
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (direction == "Up" || direction == "Down" || direction == "Right")
                            {
                                direction = "Left";
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (direction == "Left" || direction == "Right" || direction == "Down")
                            {
                                direction = "Up";
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (direction == "Left" || direction == "Right" || direction == "Up")
                            {
                                direction = "Down";
                            }
                            break;
                        case ConsoleKey.P: // Phím P để tạm dừng
                            isPaused = !isPaused; // Đảo ngược trạng thái tạm dừng (kết thúc tạm dừng)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ListenKey: {ex.Message}");
            }
        }

        // Hiển thị ra bàn chơi
        public void setUpBoard()
        {
            try
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        Console.Write(board[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in setUpBoard: {ex.Message}");
            }
        }

        // Hiển thị thực ăn
        public void PopUpfood()
        {
            try
            {
                Random random = new Random();
                int x = random.Next(1, row - 1);
                int y = random.Next(1, col - 1);
                if (x != _head.X && y != _head.Y)
                {
                    if (!foodExist)
                    {
                        food.X = x;
                        food.Y = y;
                        foodExist = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PopUpfood: {ex.Message}");
            }
        }

        // Tăng size của mảng, khởi tạo nút mới
        public void EatFood()
        {
            try
            {
                if (_head.X == food.X && _head.Y == food.Y)
                {
                    score += 1;
                    Array.Resize(ref body, body.Length + 1);
                    body[body.Length - 1] = new Point(-1, -1);
                    speed -= 20;
                    foodExist = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EatFood: {ex.Message}");
            }
        }

        // Tăng độ dài thân rắn
        public void SpawnBody()
        {
            try
            {
                for (int i = body.Length - 1; i > 0; i--)
                {
                    body[i].X = body[i - 1].X;
                    body[i].Y = body[i - 1].Y;
                }
                if (body.Length > 0)
                {
                    body[0].X = _head.X;
                    body[0].Y = _head.Y;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SpawnBody: {ex.Message}");
            }
        }

        // Hiển thị điểm
            public void ShowPoint()
        {
            try
            {
                Console.WriteLine($"Score: {score}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ShowPoint: {ex.Message}");
            }
        }
    }
}
