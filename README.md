# Minesweeper Game

This project is a Windows Forms implementation of the classic Minesweeper game written in C#. The application was built to demonstrate core programming concepts including classes, arrays, loops, conditional logic, switch cases, event handling, and file input/output.

## Features

- 6x6 game board built from a two-dimensional array
- Random mine placement with adjacent mine counting
- Left-click to reveal cells
- Right-click to place or remove flags
- Win and loss detection
- Player name and high score tracking
- High score persistence using `highscores.json`

## Project Files

- `Program.cs`: application entry point
- `MainForm.cs`: user interface behavior and event handling
- `MainForm.Designer.cs`: Windows Forms layout definition
- `MinesweeperGameEngine.cs`: board setup and game rules
- `Cell.cs`: data model for each board position
- `Player.cs`: player information and score tracking
- `HighScoreService.cs`: file-based high score loading and saving

## How To Run

Open a terminal in the project folder and run:

```powershell
dotnet run
```

To build the project without running it:

```powershell
dotnet build
```

## How The Game Works

1. Enter a player name.
2. Left-click a square to reveal it.
3. Right-click a square to place or remove a flag.
4. Revealing a mine ends the game.
5. Revealing every non-mine square wins the game.
6. The score is based on the number of safe cells revealed.
7. If the score is higher than the stored high score, it can be saved to file.

## Screenshot Checklist

Capture screenshots that show the following:

1. The main window after the program starts
2. A game in progress with several revealed squares
3. A flagged square using right-click
4. A completed win or loss screen
5. The player name and high score display

## Notes

- High scores are stored in `highscores.json` in the application output folder.
- If the score file does not exist yet, the program creates it when a score is saved.
