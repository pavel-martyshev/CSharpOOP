using Minesweeper.Core.Enums;

namespace Minesweeper.Core.Interfaces;

internal interface IGamePresenter
{
    ICell GetCell(int row, int column);

    (int, int) GetPlayingFieldSize(Difficulty difficulty);

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
