namespace MinesweeperGame;

public enum MoveResult
{
    Invalid,
    Safe,
    MineHit,
    Won
}

public class MinesweeperGameEngine
{
    private readonly Random _random = new();

    public int Rows { get; }

    public int Columns { get; }

    public int MineCount { get; }

    public int RevealedSafeCells { get; private set; }

    public bool GameOver { get; private set; }

    public Cell[,] Board { get; private set; }

    public MinesweeperGameEngine(int rows, int columns, int mineCount)
    {
        Rows = rows;
        Columns = columns;
        MineCount = mineCount;
        Board = new Cell[Rows, Columns];
        ResetGame();
    }

    public void ResetGame()
    {
        GameOver = false;
        RevealedSafeCells = 0;
        InitializeBoard();
        PlaceMines();
        CalculateAdjacentMines();
    }

    public MoveResult RevealCell(int row, int column)
    {
        if (GameOver || !IsInBounds(row, column))
        {
            return MoveResult.Invalid;
        }

        Cell selectedCell = Board[row, column];
        if (selectedCell.IsRevealed || selectedCell.IsFlagged)
        {
            return MoveResult.Invalid;
        }

        if (selectedCell.IsMine)
        {
            selectedCell.IsRevealed = true;
            RevealAllMines();
            GameOver = true;
            return MoveResult.MineHit;
        }

        FloodReveal(row, column);

        if (CheckWin())
        {
            GameOver = true;
            return MoveResult.Won;
        }

        return MoveResult.Safe;
    }

    public bool ToggleFlag(int row, int column)
    {
        if (GameOver || !IsInBounds(row, column))
        {
            return false;
        }

        Cell selectedCell = Board[row, column];
        if (selectedCell.IsRevealed)
        {
            return false;
        }

        selectedCell.IsFlagged = !selectedCell.IsFlagged;
        return true;
    }

    public bool CheckWin()
    {
        return RevealedSafeCells == (Rows * Columns) - MineCount;
    }

    private void InitializeBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Board[row, column] = new Cell();
            }
        }
    }

    private void PlaceMines()
    {
        int placedMines = 0;

        // Continue until the board contains the requested number of distinct mine locations.
        while (placedMines < MineCount)
        {
            int row = _random.Next(Rows);
            int column = _random.Next(Columns);

            if (!Board[row, column].IsMine)
            {
                Board[row, column].IsMine = true;
                placedMines++;
            }
        }
    }

    private void CalculateAdjacentMines()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                if (Board[row, column].IsMine)
                {
                    continue;
                }

                int adjacentMines = 0;

                // Scan the eight neighboring positions around the current cell.
                for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
                {
                    for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                    {
                        if (rowOffset == 0 && columnOffset == 0)
                        {
                            continue;
                        }

                        int neighborRow = row + rowOffset;
                        int neighborColumn = column + columnOffset;

                        if (IsInBounds(neighborRow, neighborColumn) && Board[neighborRow, neighborColumn].IsMine)
                        {
                            adjacentMines++;
                        }
                    }
                }

                Board[row, column].AdjacentMines = adjacentMines;
            }
        }
    }

    private void FloodReveal(int startRow, int startColumn)
    {
        Queue<(int Row, int Column)> cellsToReveal = new();
        cellsToReveal.Enqueue((startRow, startColumn));

        // A queue-based reveal avoids deep recursion and expands outward from empty cells.
        while (cellsToReveal.Count > 0)
        {
            (int row, int column) = cellsToReveal.Dequeue();
            Cell cell = Board[row, column];

            if (cell.IsRevealed || cell.IsFlagged)
            {
                continue;
            }

            cell.IsRevealed = true;
            RevealedSafeCells++;

            if (cell.AdjacentMines > 0)
            {
                continue;
            }

            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                {
                    if (rowOffset == 0 && columnOffset == 0)
                    {
                        continue;
                    }

                    int neighborRow = row + rowOffset;
                    int neighborColumn = column + columnOffset;

                    if (IsInBounds(neighborRow, neighborColumn))
                    {
                        Cell neighbor = Board[neighborRow, neighborColumn];
                        if (!neighbor.IsMine && !neighbor.IsRevealed)
                        {
                            cellsToReveal.Enqueue((neighborRow, neighborColumn));
                        }
                    }
                }
            }
        }
    }

    private void RevealAllMines()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                if (Board[row, column].IsMine)
                {
                    Board[row, column].IsRevealed = true;
                }
            }
        }
    }

    private bool IsInBounds(int row, int column)
    {
        return row >= 0 && row < Rows && column >= 0 && column < Columns;
    }
}
