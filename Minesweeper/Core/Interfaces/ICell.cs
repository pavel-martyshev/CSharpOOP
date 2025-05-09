namespace Minesweeper.Core.Interfaces;

public interface ICell
{
    bool IsMine { get; set; }

    bool IsRevealed { get; set; }

    bool IsFlagged { get; set; }

    int NeighborMinesCount { get; set; }

    bool IsDeathPlace {  get; set; }
}
