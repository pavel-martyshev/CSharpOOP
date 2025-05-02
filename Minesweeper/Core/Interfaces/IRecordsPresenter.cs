using Minesweeper.Core.Enums;

namespace Minesweeper.Core.Interfaces;

internal interface IRecordsPresenter
{
    string GetRecordsString();

    void SaveRecord((string, int, Difficulty) recordInfo);
}
