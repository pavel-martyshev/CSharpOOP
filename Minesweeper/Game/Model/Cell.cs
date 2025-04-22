using Minesweeper.Core.Interfaces;
using System.Text;

namespace Minesweeper.Game.Model;

internal class Cell : ICell
{
    private bool _isMine;

    public bool IsMine
    {
        get => _isMine;

        set
        {
            if (!_isMine)
            {
                _isMine = true;
            }
        }
    }

    public bool IsRevealed { get; set; }

    public bool IsFlagged { get; set; }

    public int NeighborMinesCount { get; set; }

    public bool IsDeathPlace { get; set; }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        if (IsMine)
        {
            stringBuilder.Append('M');
        }
        else if (IsRevealed)
        {
            stringBuilder.Append('R');
        }
        else
        {
            stringBuilder.Append('C');
        }

        return stringBuilder.ToString();
    }
}
