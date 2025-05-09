using Minesweeper.Core.Enums;

namespace Minesweeper.Core.Interfaces;

internal interface IMineField
{
    int RowsCount { get; }

    int ColumnsCount { get; }

    int Size { get; }

    int FlagsPlacedCount { get; }

    bool IsGenerated { get; }

    event Action? AllSafeCellsRevealed;

    event Action? OnMineStepped;

    void UpdateSizeByDifficulty(Difficulty difficulty);

    void GenerateNewMineField(int rowsCount, int columnsCount, int safeCellRow, int safeCellColumn);

    void ResetMineField();

    void RevealChainReaction(int rowsCount, int columnsCount);

    void Chording(int row, int column);

    void SetDeathPlace(int row, int column);

    void SetFlagged(int row, int column);

    int GetMinesLeft();

    ICell? GetCell(int row, int column);

    bool IsChordingPossible(int row, int column);

    bool IsRevealed(int row, int column);

    bool IsMine(int row, int column);

    bool IsFlagged(int row, int column);
}
