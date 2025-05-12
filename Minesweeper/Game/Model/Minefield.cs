using Minesweeper.Core.Enums;
using Minesweeper.Core.Interfaces;
using System.Text;

namespace Minesweeper.Game.Model;

internal class MineField : IMineField
{
    private ICell[,]? _cells;

    private List<(int Row, int Column)>? _minesCoordinates;

    private int _unrevealedCellsCount;

    public int RowsCount { get; private set; } = 9;

    public int ColumnsCount { get; private set; } = 9;

    public int Size => RowsCount * ColumnsCount;

    private int _minesCount;

    public int FlagsPlacedCount { get; private set; }

    public bool IsGenerated => _cells is not null;

    public event Action? AllSafeCellsRevealed;

    public event Action? OnMineStepped;

    private int GetMinesCount()
    {
        return Size switch
        {
            81 => 10,
            256 => 40,
            _ => 99
        };
    }

    private void GenerateCells(int safeCellRow, int safeCellColumn)
    {
        _cells = new Cell[RowsCount, ColumnsCount];
        _unrevealedCellsCount = Size;

        for (var i = 0; i < RowsCount; i++)
        {
            for (var j = 0; j < ColumnsCount; j++)
            {
                _cells[i, j] = new Cell();
            }
        }

        _minesCount = GetMinesCount();
        _minesCoordinates = [];

        var random = new Random();

        var readyMinesCount = 0;

        while (readyMinesCount < _minesCount)
        {
            var row = random.Next(RowsCount);
            var column = random.Next(ColumnsCount);

            var cell = _cells[row, column];

            if (!cell.IsMine && row != safeCellRow && column != safeCellColumn)
            {
                cell.IsMine = true;
                _minesCoordinates.Add((row, column));

                readyMinesCount++;
            }
        }
    }

    public void UpdateSizeByDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                RowsCount = 9;
                ColumnsCount = 9;
                break;
            case Difficulty.Medium:
                RowsCount = 16;
                ColumnsCount = 16;
                break;
            case Difficulty.Hard:
                RowsCount = 16;
                ColumnsCount = 30;
                break;
        }
    }

    public void GenerateNewMineField(int rowsCount, int columnsCount, int safeCellRow, int safeCellColumn)
    {
        ResetMineField();

        RowsCount = rowsCount;
        ColumnsCount = columnsCount;

        GenerateCells(safeCellRow, safeCellColumn);
    }

    public void ResetMineField()
    {
        _cells = default;
        FlagsPlacedCount = 0;
    }

    private bool IsInsideField(int row, int column)
    {
        return row >= 0 && row < RowsCount && column >= 0 && column < ColumnsCount;
    }

    public void RevealChainReaction(int row, int column)
    {
        if (_cells is null)
        {
            return;
        }

        var queue = new Queue<(int Row, int Column)>();
        queue.Enqueue((row, column));

        while (queue.Count > 0)
        {
            var (currentRow, currentColumn) = queue.Dequeue();
            var currentCell = _cells![currentRow, currentColumn];

            if (currentCell.IsRevealed || currentCell.IsFlagged)
            {
                continue;
            }

            currentCell.IsRevealed = true;
            _unrevealedCellsCount--;

            var startNeighborRow = currentRow - 1;
            var endNeighborRow = currentRow + 1;

            var startNeighborColumn = currentColumn - 1;
            var endNeighborColumn = currentColumn + 1;

            for (var i = startNeighborRow; i <= endNeighborRow; i++)
            {
                for (var j = startNeighborColumn; j <= endNeighborColumn; j++)
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

            for (var i = startNeighborRow; i <= endNeighborRow; i++)
            {
                for (var j = startNeighborColumn; j <= endNeighborColumn; j++)
                {
                    if (IsInsideField(i, j) && !_cells[i, j].IsMine)
                    {
                        queue.Enqueue((i, j));
                    }
                }
            }
        }

        if (_unrevealedCellsCount == _minesCount)
        {
            RevealAllUnflaggedMines();

            AllSafeCellsRevealed!();
        }
    }

    public void Chording(int row, int column)
    {
        if (_cells is null)
        {
            return;
        }

        var currentCell = _cells![row, column];

        var startNeighborRow = row - 1;
        var endNeighborRow = row + 1;

        var startNeighborColumn = column - 1;
        var endNeighborColumn = column + 1;

        int flaggedMines = 0;
        var cellsToRevealCoordinates = new List<(int rowToReveal, int columnToReveal)>();

        for (var i = startNeighborRow; i <= endNeighborRow; i++)
        {
            for (var j = startNeighborColumn; j <= endNeighborColumn; j++)
            {
                if (!IsInsideField(i, j))
                {
                    continue;
                }

                var cell = _cells[i, j];

                if (cell.IsRevealed)
                {
                    continue;
                }

                if (cell.IsMine && !cell.IsFlagged)
                {
                    SetDeathPlace(i, j);
                    OnMineStepped?.Invoke();
                    return;
                }

                if (cell.IsMine && cell.IsFlagged)
                {
                    flaggedMines++;
                }
                else
                {
                    cellsToRevealCoordinates.Add((i, j));
                }
            }
        }

        if (currentCell.NeighborMinesCount == flaggedMines)
        {
            foreach (var (rowToReveal, columnToReveal) in cellsToRevealCoordinates)
            {
                RevealChainReaction(rowToReveal, columnToReveal);
            }
        }
    }

    public void SetDeathPlace(int row, int column)
    {
        if (_cells is null)
        {
            return;
        }

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
        if (_cells is null)
        {
            return;
        }

        var cell = _cells![row, column];

        if (cell.IsFlagged)
        {
            cell.IsFlagged = false;
            FlagsPlacedCount--;
        }
        else if (!cell.IsFlagged && FlagsPlacedCount != _minesCount)
        {
            cell.IsFlagged = true;
            FlagsPlacedCount++;
        }
    }

    public int GetMinesLeft()
    {
        return _minesCount - FlagsPlacedCount;
    }

    public ICell? GetCell(int row, int column)
    {
        if (_cells is null)
        {
            return null;
        }

        return _cells[row, column];
    }

    public bool IsChordingPossible(int row, int column)
    {
        if (_cells is null)
        {
            return false;
        }

        var currentCell = _cells[row, column];

        if (!currentCell.IsRevealed)
        {
            return false;
        }

        var startNeighborRow = row - 1;
        var endNeighborRow = row + 1;

        var startNeighborColumn = column - 1;
        var endNeighborColumn = column + 1;

        var flagsCount = 0;

        for (var i = startNeighborRow; i <= endNeighborRow; i++)
        {
            for (var j = startNeighborColumn; j <= endNeighborColumn; j++)
            {
                if (!IsInsideField(i, j))
                {
                    continue;
                }

                if (_cells[i, j].IsFlagged)
                {
                    flagsCount++;
                }
            }
        }

        return flagsCount == currentCell.NeighborMinesCount;
    }

    public bool IsRevealed(int row, int column)
    {
        if (_cells is null)
        {
            return false;
        }

        return _cells![row, column].IsRevealed;
    }

    public bool IsMine(int row, int column)
    {
        if (_cells is null)
        {
            return false;
        }

        return _cells![row, column].IsMine;
    }

    public bool IsFlagged(int row, int column)
    {
        if (_cells is null)
        {
            return false;
        }

        return _cells![row, column].IsFlagged;
    }

    public override string ToString()
    {
        if (_cells is null)
        {
            return string.Empty;
        }

        var stringBuilder = new StringBuilder();

        for (var i = 0; i < RowsCount; i++)
        {
            for (var j = 0; j < ColumnsCount; j++)
            {
                stringBuilder.Append($"{_cells![i, j]} ");
            }

            stringBuilder.Append(Environment.NewLine);
        }

        return stringBuilder.ToString();
    }
}
