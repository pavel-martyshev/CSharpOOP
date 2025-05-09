using Minesweeper.Core.Interfaces;

namespace Minesweeper.Game.Model;

class Cell : ICell
{
    public bool IsMine { get; set; }

    public bool IsRevealed { get; set; }

    public bool IsFlagged { get; set; }

    public int NeighborMinesCount { get; set; }

    public bool IsDeathPlace { get; set; }

    public Cell() {}

    public Cell(bool isMine, bool isRevealed, bool isFlagged, int neighborMinesCount, bool isDeathPlace)
    {
        IsMine = isMine;
        IsRevealed = isRevealed;
        IsFlagged = isFlagged;
        NeighborMinesCount = neighborMinesCount;
        IsDeathPlace = isDeathPlace;
    }

    public override string ToString() => IsMine ? "M" : (IsRevealed ? "R" : "C");
}
