using Minesweeper.Core.Enums;

namespace Minesweeper.Core.Interfaces;

internal interface IPresenter
{
    (bool IsRevealed, bool IsFlagged, bool IsMine, bool IsDeathPlace, int NeighborMinesCount) GetCellState(int row, int column);

    void OnCellLeftClickHandler(int row, int column);

    void OnCellRightClickHandler(int row, int column);

    void DifficultyChangeHandler(int newRowsCount, int newColumnsCount);

    void AllSafeRevealedCellsHandler();

    string GetRecordsString();

    void Restart();

    string GetAboutInfo();

    void SaveRecord((string, int, Difficulty) recordInfo);

    void Run();
}
