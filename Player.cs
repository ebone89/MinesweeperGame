namespace MinesweeperGame;

public class Player
{
    public string Name { get; set; }

    public int HighScore { get; private set; }

    public Player(string name, int highScore = 0)
    {
        Name = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();
        HighScore = highScore;
    }

    // Update the stored score only when the current game exceeds the previous best.
    public bool UpdateHighScore(int score)
    {
        if (score <= HighScore)
        {
            return false;
        }

        HighScore = score;
        return true;
    }
}
