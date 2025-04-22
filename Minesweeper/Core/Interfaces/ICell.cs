namespace Minesweeper.Core.Interfaces;

internal interface ICell
{
    bool IsMine { get; set; }

    bool IsRevealed { get; set; }

    bool IsFlagged { get; set; }

    int NeighborMinesCount { get; set; }

    bool IsDeathPlace {  get; set; }
}
