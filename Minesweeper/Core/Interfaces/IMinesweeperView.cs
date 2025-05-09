using Minesweeper.Core.Enums;

namespace Minesweeper.Core.Interfaces;

internal interface IMinesweeperView
{
    int CellSideBaseSize { get; }

    bool IsGameOver { get; }

    event Func<int, int, ICell?>? RequestCell;
    event Func<Difficulty, (int, int)>? RequestPlayingFieldSize;

    event Func<string>? RequestAboutInfo;
    event Func<string>? RequestRecordsString;

    event Action? Restart;
    event Action<int, int>? OnCellLeftClick;

    event Action<int, int>? OnCellRightClick;
    event Action<int, int>? OnDifficultyChange;

    event Action<(string, int, Difficulty)>? SaveRecord;
    event Action? TimerStopRequest;
    event Action<int, int>? OnCellMiddleClick;

    void RedrawCell(int row, int column);

    void InvalidatePlayingField();

    void SetMinesCount(int count);

    void SetElapsedSeconds(int elapsedSecond);

    void SetGameOver();

    void SetVictory(int elapsedSeconds);
}
