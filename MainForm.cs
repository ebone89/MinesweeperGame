namespace MinesweeperGame;

public partial class MainForm : Form
{
    private const int Rows = 6;
    private const int Columns = 6;
    private const int Mines = 8;

    private readonly MinesweeperGameEngine _game;
    private readonly HighScoreService _highScoreService;
    private readonly Button[,] _boardButtons;

    private Player _player;

    public MainForm()
    {
        InitializeComponent();

        string scoreFilePath = Path.Combine(AppContext.BaseDirectory, "highscores.json");
        _highScoreService = new HighScoreService(scoreFilePath);
        _player = new Player("Player");
        _game = new MinesweeperGameEngine(Rows, Columns, Mines);
        _boardButtons = new Button[Rows, Columns];

        ConfigureBoardLayout();
        CreateBoardButtons();
        LoadPlayerScore();
        UpdateBoardDisplay();
    }

    private void ConfigureBoardLayout()
    {
        // Match the visual grid to the dimensions used by the game engine.
        boardTableLayoutPanel.ColumnStyles.Clear();
        boardTableLayoutPanel.RowStyles.Clear();

        for (int column = 0; column < Columns; column++)
        {
            boardTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / Columns));
        }

        for (int row = 0; row < Rows; row++)
        {
            boardTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / Rows));
        }
    }

    private void CreateBoardButtons()
    {
        boardTableLayoutPanel.Controls.Clear();

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Button boardButton = new()
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                    // Store the board coordinates on each button so the shared event handler
                    // can map clicks back to the correct game cell.
                    Tag = new Point(row, column),
                    BackColor = SystemColors.ControlLight
                };

                boardButton.MouseUp += BoardButton_MouseUp;
                _boardButtons[row, column] = boardButton;
                boardTableLayoutPanel.Controls.Add(boardButton, column, row);
            }
        }
    }

    private void BoardButton_MouseUp(object? sender, MouseEventArgs e)
    {
        if (sender is not Button boardButton || boardButton.Tag is not Point position)
        {
            return;
        }

        switch (e.Button)
        {
            case MouseButtons.Left:
                HandleRevealMove(position.X, position.Y);
                break;
            case MouseButtons.Right:
                HandleFlagMove(position.X, position.Y);
                break;
        }
    }

    private void HandleRevealMove(int row, int column)
    {
        EnsureCurrentPlayer();
        MoveResult result = _game.RevealCell(row, column);

        switch (result)
        {
            case MoveResult.Invalid:
                statusValueLabel.Text = "That square cannot be opened right now.";
                break;
            case MoveResult.Safe:
                statusValueLabel.Text = "Safe move. Continue clearing the board.";
                break;
            case MoveResult.MineHit:
                statusValueLabel.Text = "Mine triggered. Start a new round to try again.";
                break;
            case MoveResult.Won:
                int score = _game.RevealedSafeCells;
                statusValueLabel.Text = $"Board cleared. Final score: {score}.";
                if (_player.UpdateHighScore(score))
                {
                    highScoreValueLabel.Text = _player.HighScore.ToString();
                    _highScoreService.SaveHighScore(_player);
                    statusValueLabel.Text = $"Board cleared. New best score: {score}.";
                }

                break;
            default:
                statusValueLabel.Text = "Board updated.";
                break;
        }

        UpdateBoardDisplay();
    }

    private void HandleFlagMove(int row, int column)
    {
        bool changed = _game.ToggleFlag(row, column);
        if (changed)
        {
            statusValueLabel.Text = "Flag status updated.";
            UpdateBoardDisplay();
        }
    }

    private void UpdateBoardDisplay()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Cell cell = _game.Board[row, column];
                Button boardButton = _boardButtons[row, column];

                string buttonText;

                // A switch keeps the visual state rules in one place and satisfies
                // the assignment requirement to use switch cases in the application logic.
                switch (GetCellDisplayState(cell))
                {
                    case "Hidden":
                        buttonText = string.Empty;
                        boardButton.Enabled = true;
                        boardButton.BackColor = SystemColors.ControlLight;
                        break;
                    case "Flagged":
                        buttonText = "F";
                        boardButton.Enabled = true;
                        boardButton.BackColor = Color.Khaki;
                        break;
                    case "Mine":
                        buttonText = "*";
                        boardButton.Enabled = false;
                        boardButton.BackColor = Color.LightCoral;
                        break;
                    case "Number":
                        buttonText = cell.AdjacentMines.ToString();
                        boardButton.Enabled = false;
                        boardButton.BackColor = Color.White;
                        break;
                    default:
                        buttonText = string.Empty;
                        boardButton.Enabled = false;
                        boardButton.BackColor = Color.WhiteSmoke;
                        break;
                }

                boardButton.Text = buttonText;
            }
        }
    }

    private static string GetCellDisplayState(Cell cell)
    {
        if (cell.IsFlagged && !cell.IsRevealed)
        {
            return "Flagged";
        }

        if (!cell.IsRevealed)
        {
            return "Hidden";
        }

        if (cell.IsMine)
        {
            return "Mine";
        }

        return cell.AdjacentMines > 0 ? "Number" : "Empty";
    }

    private void LoadPlayerScore()
    {
        string playerName = GetPlayerName();
        int highScore = _highScoreService.LoadHighScore(playerName);
        _player = new Player(playerName, highScore);
        playerNameTextBox.Text = _player.Name;
        highScoreValueLabel.Text = _player.HighScore.ToString();
    }

    private void EnsureCurrentPlayer()
    {
        string playerName = GetPlayerName();

        if (string.Equals(_player.Name, playerName, StringComparison.OrdinalIgnoreCase))
        {
            if (playerNameTextBox.Text != playerName)
            {
                playerNameTextBox.Text = playerName;
            }

            return;
        }

        int highScore = _highScoreService.LoadHighScore(playerName);
        _player = new Player(playerName, highScore);
        playerNameTextBox.Text = _player.Name;
        highScoreValueLabel.Text = _player.HighScore.ToString();
    }

    private string GetPlayerName()
    {
        return string.IsNullOrWhiteSpace(playerNameTextBox.Text) ? "Player" : playerNameTextBox.Text.Trim();
    }

    private void PlayerNameTextBox_Leave(object? sender, EventArgs e)
    {
        EnsureCurrentPlayer();
    }

    private void ResetButton_Click(object? sender, EventArgs e)
    {
        LoadPlayerScore();
        _game.ResetGame();
        statusValueLabel.Text = "New board ready.";
        UpdateBoardDisplay();
    }

    private void SaveScoreButton_Click(object? sender, EventArgs e)
    {
        EnsureCurrentPlayer();
        // Saving also preserves progress from a partial game if it exceeds the previous record.
        _player = new Player(GetPlayerName(), Math.Max(_player.HighScore, _game.RevealedSafeCells));
        _highScoreService.SaveHighScore(_player);
        highScoreValueLabel.Text = _player.HighScore.ToString();
        statusValueLabel.Text = "Score saved successfully.";
    }
}
