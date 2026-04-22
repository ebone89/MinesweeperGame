using System.Text.Json;

namespace MinesweeperGame;

public class HighScoreService
{
    private readonly string _filePath;

    public HighScoreService(string filePath)
    {
        _filePath = filePath;
    }

    public int LoadHighScore(string playerName)
    {
        Dictionary<string, int> scores = ReadScores();
        return scores.TryGetValue(NormalizeName(playerName), out int score) ? score : 0;
    }

    public void SaveHighScore(Player player)
    {
        Dictionary<string, int> scores = ReadScores();
        string playerName = NormalizeName(player.Name);

        if (!scores.TryGetValue(playerName, out int existingScore) || player.HighScore > existingScore)
        {
            scores[playerName] = player.HighScore;
            WriteScores(scores);
        }
    }

    private Dictionary<string, int> ReadScores()
    {
        if (!File.Exists(_filePath))
        {
            return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            Dictionary<string, int>? scores =
                JsonSerializer.Deserialize<Dictionary<string, int>>(json);

            return scores is null
                ? new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
                : new Dictionary<string, int>(scores, StringComparer.OrdinalIgnoreCase);
        }
        catch
        {
            // If the file is missing or unreadable, fall back to an empty score table.
            return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }
    }

    private void WriteScores(Dictionary<string, int> scores)
    {
        string directory = Path.GetDirectoryName(_filePath) ?? AppContext.BaseDirectory;
        Directory.CreateDirectory(directory);

        string json = JsonSerializer.Serialize(scores, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_filePath, json);
    }

    private static string NormalizeName(string playerName)
    {
        return string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName.Trim();
    }
}
