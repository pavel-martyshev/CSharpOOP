using Minesweeper.Core.Enums;

namespace Minesweeper.Core.Interfaces;

internal interface IGamePresenter
{
    (bool IsRevealed, bool IsFlagged, bool IsMine, bool IsDeathPlace, int NeighborMinesCount) GetCellState(int row, int column);

    void OnCellLeftClickHandler(int row, int column);

    void OnCellRightClickHandler(int row, int column);

    void OnCellMiddleClickHandler(int row, int column);

    void OnMineSteppedHandler();

    void DifficultyChangeHandler(int newRowsCount, int newColumnsCount);

    void AllSafeRevealedCellsHandler();

    void OnTimerTick(int elapsedSeconds);

    void Restart();

    string GetAboutInfo();

    void Run();
}
