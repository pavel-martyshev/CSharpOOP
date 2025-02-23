using System.Text;
using VectorTask;

namespace MatrixTask;

class Matrix
{
    private Vector[] grid;

    public int RowsCount { get; private set; }

    public int ColumnsCount { get; private set; }

    public bool IsSquareMatrix { get; }

    public Matrix(int rowsCount, int columnsCount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(rowsCount, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(columnsCount, 0);

        grid = new Vector[rowsCount];

        for (int i = 0; i < rowsCount; i++)
        {
            grid[i] = new Vector(columnsCount);
        }

        RowsCount = rowsCount;
        ColumnsCount = columnsCount;
        IsSquareMatrix = rowsCount == columnsCount;
    }

    public Matrix(Matrix matrix)
    {
        grid = new Vector[matrix.RowsCount];

        for (int i = 0; i < matrix.RowsCount; i++)
        {
            grid[i] = new Vector(matrix.grid[i]);
        }

        RowsCount = matrix.RowsCount;
        ColumnsCount = matrix.ColumnsCount;
        IsSquareMatrix = matrix.RowsCount == matrix.ColumnsCount;
    }

    public Matrix(double[,] matrixArray)
    {
        int rows = matrixArray.GetLength(0);
        int columns = matrixArray.GetLength(1);

        ArgumentOutOfRangeException.ThrowIfEqual(rows, 0);
        ArgumentOutOfRangeException.ThrowIfEqual(columns, 0);

        grid = new Vector[rows];

        for (int i = 0; i < rows; i++)
        {
            double[] rowArray = new double[columns];

            for (int j = 0; j < columns; j++)
            {
                rowArray[j] = matrixArray[i, j];
            }

            grid[i] = new Vector(rowArray);
        }

        RowsCount = rows;
        ColumnsCount = columns;
        IsSquareMatrix = rows == columns;
    }

    public Matrix(Vector[] vectorsArray)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(vectorsArray.Length, 0);

        grid = [.. vectorsArray];

        int maxVectorSize = vectorsArray.Aggregate(vectorsArray[0].Size, (maxSize, vector) => Math.Max(maxSize, vector.Size));

        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].Size < maxVectorSize)
            {
                grid[i].Add(new Vector([.. new double[maxVectorSize]]));
            }
        }

        RowsCount = vectorsArray.Length;
        ColumnsCount = maxVectorSize;
        IsSquareMatrix = vectorsArray.Length == maxVectorSize;
    }

    public Vector this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, RowsCount - 1);

            return new Vector(grid[index]);
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, RowsCount - 1);

            grid[index] = new Vector(value);
        }
    }

    private static void EnsureSameSize(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.RowsCount != matrix2.RowsCount || matrix1.ColumnsCount != matrix2.ColumnsCount)
        {
            throw new ArgumentException($"The matrices must be the same size ({matrix1.RowsCount}x{matrix1.ColumnsCount} | {matrix2.RowsCount}x{matrix2.ColumnsCount}).");
        }
    }

    public static Matrix GetMatricesAddition(Matrix matrix1, Matrix matrix2)
    {
        EnsureSameSize(matrix1, matrix2);

        Matrix matrix = new(matrix1);
        matrix.Add(matrix2);

        return matrix;
    }

    public static Matrix GetMatricesSubtract(Matrix matrix1, Matrix matrix2)
    {
        EnsureSameSize(matrix1, matrix2);

        Matrix matrix = new(matrix1);
        matrix.Subtract(matrix2);

        return matrix;
    }

    public static Matrix GetMatricesMultiply(Matrix matrix1, Matrix matrix2)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(matrix1.ColumnsCount, matrix2.RowsCount);

        Matrix multiplicationProduct = new(matrix1.RowsCount, matrix2.ColumnsCount);

        for (int i = 0; i < matrix1.RowsCount; i++)
        {
            for (int j = 0; j < matrix2.ColumnsCount; j++)
            {
                multiplicationProduct[i][j] = Vector.GetDotProduct(matrix1[i], matrix2.GetColumnByIndex(j));
            }
        }

        return multiplicationProduct;
    }

    public Vector GetColumnByIndex(int index)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);

        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, ColumnsCount - 1);

        double[] columnComponents = new double[RowsCount];

        for (int i = 0; i < RowsCount; i++)
        {
            columnComponents[i] = grid[i][index];
        }

        return new Vector([.. columnComponents]);
    }

    public void Transpose()
    {
        Vector[] transposedMatrixGrid = new Vector[ColumnsCount];

        for (int i = 0; i < ColumnsCount; i++)
        {
            transposedMatrixGrid[i] = GetColumnByIndex(i);
        }

        grid = transposedMatrixGrid;
    }

    public void MultiplyByScalar(double scalar)
    {
        foreach (Vector vector in grid)
        {
            vector.MultiplyByScalar(scalar);
        }
    }

    public double GetDeterminant()
    {
        if (!IsSquareMatrix)
        {
            throw new InvalidOperationException($"Cannot compute the determinant of a non-square matrix ({RowsCount}x{ColumnsCount}).");
        }

        return GetDeterminant(this);
    }

    private static double GetDeterminant(Matrix matrix)
    {
        int size = matrix.RowsCount;

        if (size == 1)
        {
            return matrix[0][0];
        }

        if (size == 2)
        {
            return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
        }

        double determinant = 0;

        for (int expansionColumnIndex = 0; expansionColumnIndex < size; expansionColumnIndex++)
        {
            Matrix minor = GetMinor(matrix, expansionColumnIndex);
            double sign = (expansionColumnIndex % 2 == 0) ? 1 : -1;

            determinant += sign * matrix[0][expansionColumnIndex] * GetDeterminant(minor);
        }

        return determinant;
    }

    private static Matrix GetMinor(Matrix matrix, int excludedColumnIndex)
    {
        int size = matrix.RowsCount;
        double[,] minor = new double[size - 1, size - 1];

        for (int i = 1; i < size; i++)
        {
            int minorColumn = 0;

            for (int j = 0; j < size; j++)
            {
                if (j == excludedColumnIndex)
                {
                    continue;
                }

                minor[i - 1, minorColumn] = matrix[i][j];
                minorColumn++;
            }
        }

        return new Matrix(minor);
    }

    public void MultiplyByVector(Vector vector)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(vector.Size, ColumnsCount);

        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < ColumnsCount; j++)
            {
                grid[i][j] *= vector[j];
            }
        }
    }

    public void Add(Matrix matrix)
    {
        EnsureSameSize(this, matrix);

        for (int i = 0; i < RowsCount; i++)
        {
            grid[i].Add(matrix.grid[i]);
        }
    }

    public void Subtract(Matrix matrix)
    {
        EnsureSameSize(this, matrix);

        for (int i = 0; i < RowsCount; i++)
        {
            grid[i].Subtract(matrix.grid[i]);
        }
    }

    public override string ToString()
    {
        StringBuilder matrixString = new();
        matrixString.Append('{');

        foreach (Vector vector in grid)
        {
            matrixString.Append($"{vector}, ");
        }

        matrixString.Length -= 2;
        matrixString.Append('}');

        return matrixString.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        Matrix matrix = (Matrix)obj;

        if (RowsCount != matrix.RowsCount || ColumnsCount != matrix.ColumnsCount)
        {
            return false;
        }

        for (int i = 0; i < RowsCount; i++)
        {
            if (!grid[i].Equals(matrix[i]))
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        const int prime = 31;
        int hash = 1;

        foreach (Vector vector in grid)
        {
            hash = prime * hash + vector.GetHashCode();
        }

        return hash;
    }
}
