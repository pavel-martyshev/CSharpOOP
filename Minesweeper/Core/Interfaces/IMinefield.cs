namespace Minesweeper.Core.Interfaces;

internal interface IMinefield
{
    int RowsCount { get; }

    int ColumnsCount { get; }

    int Size { get; }

    int FlagsPlaced { get; }

    event Action? AllSafeCellsRevealed;

    void GenerateNewMineField(int rowsCount, int columnsCount);

    void RevealChainReaction(int rowsCount, int columnsCount);

    void SetDeathPlace(int row, int column);

    void SetFlagged(int row, int column);

    int GetMinesLeft();

    (bool IsRevealed, bool IsFlagged, bool IsMine, bool IsDeathPlace, int NeighborMinesCount) GetCellProperties(int row, int column);

    bool IsRevealed(int row, int column);

    bool IsMine(int row, int column);
}
