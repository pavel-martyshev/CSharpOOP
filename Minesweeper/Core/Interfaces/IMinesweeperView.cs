using Minesweeper.Core.Enums;

namespace Minesweeper.Core.Interfaces;

internal interface IMinesweeperView
{
    int CellSideBaseSize { get; }

    int PlayingFieldRowsCount { get; }

    int PlayingFieldColumnsCount { get; }

    int ElapsedSeconds { get; }

    bool IsGameOver { get; }

    event Func<int, int, (bool IsRevealed, bool IsFlagged, bool IsMine, bool IsDeathPlace, int NeighborMinesCount)>? RequestCellState;
    event Action? Restart;

    event Action<int, int>? OnCellLeftClick;
    event Action<int, int>? OnCellRightClick;

    event Action<int, int>? OnDifficultyChange;
    event Action<(string, int, Difficulty)>? SaveRecord;

    event Func<string>? RequestAboutInfo;
    event Func<string>? RequestRecordsString;

    void RedrawCell(int row, int column);

    void InvalidatePlayingField();

    void SetMinesCount(int count);

    void StartTimer();

    void SetGameOver();

    void SetVictory();
}
