using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace ransanmoi
{
    public class ScoreManager
{
    public static void SaveScores(List<Score> scores, string filePath)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(scores);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine("Scores saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving scores: {ex.Message}");
        }
    }

    public static List<Score> LoadScores(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                List<Score>? scores = JsonSerializer.Deserialize<List<Score>>(jsonString);
                Console.WriteLine("Scores loaded successfully!");
                return scores;
            }
            else
            {
                Console.WriteLine("Score file not found. Starting with an empty score list.");
                return new List<Score>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scores: {ex.Message}");
            return new List<Score>();
        }
    }
}
}