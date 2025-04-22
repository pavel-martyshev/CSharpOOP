using Minesweeper.Core.Interfaces;
using System.Text;

namespace Minesweeper.Game.Model;

internal class Minefield : IMinefield
{
    private ICell[,]? _cells;

    private List<(int Row, int Column)>? _minesCoordinates;

    private int _unrevealedCells;

    public int RowsCount { get; private set; }

    public int ColumnsCount { get; private set; }

    public int Size => RowsCount * ColumnsCount;

    private int _minesCount;

    public int FlagsPlaced { get; private set; }

    public event Action? AllSafeCellsRevealed;

    private void ValidateCells()
    {
        if (_cells is null)
        {
            throw new InvalidOperationException("Minefield not generated");
        }
    }

    private int CalculateMinesCount()
    {
        return Size switch
        {
            81 => 10,
            256 => 40,
            _ => 99
        };
    }

    private void GenerateCells()
    {
        _cells = new Cell[RowsCount, ColumnsCount];
        _unrevealedCells = Size;

        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < ColumnsCount; j++)
            {
                _cells[i, j] = new Cell();
            }
        }

        _minesCount = CalculateMinesCount();
        _minesCoordinates = [];

        var random = new Random();

        int readyMinesCount = 0;

        while (readyMinesCount < _minesCount)
        {
            int row = random.Next(RowsCount);
            int col = random.Next(ColumnsCount);

            ICell cell = _cells[row, col];

            if (!cell.IsMine)
            {
                cell.IsMine = true;
                _minesCoordinates.Add((row, col));

                readyMinesCount++;
            }
        }
    }

    public void GenerateNewMineField(int rowsCount, int columnsCount)
    {
        RowsCount = rowsCount;
        ColumnsCount = columnsCount;
        FlagsPlaced = 0;

        GenerateCells();
    }

    private bool IsInsideField(int row, int column)
    {
        return row >= 0 && row < RowsCount && column >= 0 && column < ColumnsCount;
    }

    public void RevealChainReaction(int row, int column)
    {
        ValidateCells();

        Queue<(int Row, int Column)> queue = new();
        queue.Enqueue((row, column));

        while (queue.Count > 0)
        {
            var (currentRow, currentColumn) = queue.Dequeue();
            ICell currentCell = _cells![currentRow, currentColumn];

            if (currentCell.IsRevealed || currentCell.IsFlagged)
            {
                continue;
            }

            currentCell.IsRevealed = true;
            _unrevealedCells--;

            int startNeighborRow = currentRow - 1;
            int endNeighborRow = currentRow + 1;

            int startNeighborColumn = currentColumn - 1;
            int endNeighborColumn = currentColumn + 1;

            for (int i = startNeighborRow; i <= endNeighborRow; i++)
            {
                for (int j = startNeighborColumn; j <= endNeighborColumn; j++)
                {
                    if (IsInsideField(i, j) && _cells[i, j].IsMine)
                    {
                        currentCell.NeighborMinesCount++;
                    }
                }
            }

            if (currentCell.NeighborMinesCount != 0)
            {
                continue;
            }

            for (int i = startNeighborRow; i <= endNeighborRow; i++)
            {
                for (int j = startNeighborColumn; j <= endNeighborColumn; j++)
                {
                    if (IsInsideField(i, j) && !_cells[i, j].IsMine)
                    {
                        queue.Enqueue((i, j));
                    }
                }
            }
        }

        if (_unrevealedCells == _minesCount)
        {
            RevealAllUnflaggedMines();

            AllSafeCellsRevealed!();
        }
    }

    public void SetDeathPlace(int row, int column)
    {
        ValidateCells();

        _cells![row, column].IsDeathPlace = true;

        RevealAllUnflaggedMines();
    }

    private void RevealAllUnflaggedMines()
    {
        foreach (var (mineRow, mineColumn) in _minesCoordinates!)
        {
            if (!_cells![mineRow, mineColumn].IsFlagged)
            {
                _cells![mineRow, mineColumn].IsRevealed = true;
            }
        }
    }

    public void SetFlagged(int row, int column)
    {
        ValidateCells();

        ICell cell = _cells![row, column];

        if (cell.IsFlagged)
        {
            cell.IsFlagged = false;
            FlagsPlaced--;
        }
        else if (!cell.IsFlagged && FlagsPlaced != _minesCount)
        {
            cell.IsFlagged = true;
            FlagsPlaced++;
        }
    }

    public int GetMinesLeft()
    {
        return _minesCount - FlagsPlaced;
    }

    public (bool IsRevealed, bool IsFlagged, bool IsMine, bool IsDeathPlace, int NeighborMinesCount) GetCellProperties(int row, int column)
    {
        ValidateCells();

        return (
            _cells![row, column].IsRevealed,
            _cells[row, column].IsFlagged,
            _cells[row, column].IsMine,
            _cells[row, column].IsDeathPlace,
            _cells[row, column].NeighborMinesCount
        );
    }

    public bool IsRevealed(int row, int column)
    {
        ValidateCells();

        return _cells![row, column].IsRevealed;
    }

    public bool IsMine(int row, int column)
    {
        ValidateCells();

        return _cells![row, column].IsMine;
    }

    public override string ToString()
    {
        ValidateCells();

        StringBuilder stringBuilder = new();

        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < ColumnsCount; j++)
            {
                stringBuilder.Append($"{_cells![i, j]} ");
            }

            stringBuilder.Append(Environment.NewLine);
        }

        return stringBuilder.ToString();
    }
}
