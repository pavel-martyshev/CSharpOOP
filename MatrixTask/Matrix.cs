using System.Text;
using VectorTask;

namespace MatrixTask;

public class Matrix
{
    private Vector[] _rows;

    public int RowsCount => _rows.Length;

    public int ColumnsCount => _rows[0].Size;

    public bool IsSquare => RowsCount == ColumnsCount;

    public Matrix(int rowsCount, int columnsCount)
    {
        if (rowsCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rowsCount), "The number of rows must be greater than 0.");
        }

        if (columnsCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columnsCount), "The number of columns must be greater than 0.");
        }

        _rows = new Vector[rowsCount];

        for (int i = 0; i < rowsCount; i++)
        {
            _rows[i] = new Vector(columnsCount);
        }
    }

    public Matrix(Matrix matrix)
    {
        _rows = new Vector[matrix.RowsCount];

        for (int i = 0; i < matrix.RowsCount; i++)
        {
            _rows[i] = new Vector(matrix._rows[i]);
        }
    }

    public Matrix(double[,] matrixArray)
    {
        int rowsCount = matrixArray.GetLength(0);

        if (rowsCount <= 0)
        {
            throw new ArgumentException("The number of rows must be greater than 0.", nameof(matrixArray));
        }

        int columnsCount = matrixArray.GetLength(1);

        if (columnsCount <= 0)
        {
            throw new ArgumentException("The number of columns must be greater than 0.", nameof(matrixArray));
        }

        _rows = new Vector[rowsCount];

        for (int i = 0; i < rowsCount; i++)
        {
            double[] rowArray = new double[columnsCount];

            for (int j = 0; j < columnsCount; j++)
            {
                rowArray[j] = matrixArray[i, j];
            }

            _rows[i] = new Vector(rowArray);
        }
    }

    public Matrix(Vector[] vectorsArray)
    {
        if (vectorsArray.Length == 0)
        {
            throw new ArgumentException("The array cannot be empty.", nameof(vectorsArray));
        }

        int maxVectorSize = vectorsArray.Aggregate(vectorsArray[0].Size, (maxSize, vector) => Math.Max(maxSize, vector.Size));

        _rows = new Vector[vectorsArray.Length];

        for (int i = 0; i < vectorsArray.Length; i++)
        {
            Vector newVector = new(vectorsArray[i]);

            if (newVector.Size < maxVectorSize)
            {
                newVector.Add(new Vector(maxVectorSize));
            }

            _rows[i] = newVector;
        }
    }

    public Vector this[int index]
    {
        get
        {
            if (index < 0 || index >= _rows.Length)
            {
                throw new IndexOutOfRangeException($"The index ({index}) must be greater than 0 and less than the length ({_rows.Length}).");
            }

            return new Vector(_rows[index]);
        }

        set
        {
            if (index < 0 || index >= _rows.Length)
            {
                throw new IndexOutOfRangeException($"The index ({index}) must be greater than 0 and less than the length ({_rows.Length}).");
            }

            if (value.Size != ColumnsCount)
            {
                throw new ArgumentException($"The vector size ({value.Size}) must be equal to {ColumnsCount}.", nameof(value));
            }

            _rows[index] = new Vector(value);
        }
    }

    private static void CheckSameSize(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.RowsCount != matrix2.RowsCount || matrix1.ColumnsCount != matrix2.ColumnsCount)
        {
            throw new ArgumentException($"The matrices must be the same size. (matrix1 - {matrix1.RowsCount}x{matrix1.ColumnsCount}, matrix2 - {matrix2.RowsCount}x{matrix2.ColumnsCount}).");
        }
    }

    public static Matrix GetSum(Matrix matrix1, Matrix matrix2)
    {
        CheckSameSize(matrix1, matrix2);

        Matrix matrix = new(matrix1);
        matrix.Add(matrix2);

        return matrix;
    }

    public static Matrix GetDifference(Matrix matrix1, Matrix matrix2)
    {
        CheckSameSize(matrix1, matrix2);

        Matrix matrix = new(matrix1);
        matrix.Subtract(matrix2);

        return matrix;
    }

    public static Matrix GetProduct(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.ColumnsCount != matrix2.RowsCount)
        {
            throw new ArgumentException($"The number of columns of matrix1 ({matrix1.ColumnsCount}) must be equal to the number of rows of matrix2 ({matrix2.RowsCount}).");
        }

        Matrix product = new(matrix1.RowsCount, matrix2.ColumnsCount);

        for (int i = 0; i < matrix1.RowsCount; i++)
        {
            for (int j = 0; j < matrix2.ColumnsCount; j++)
            {
                product._rows[i][j] = Vector.GetDotProduct(matrix1._rows[i], matrix2.GetColumnByIndex(j));
            }
        }

        return product;
    }

    public Vector GetColumnByIndex(int index)
    {
        if (index < 0 || index >= ColumnsCount)
        {
            throw new IndexOutOfRangeException($"The index ({index}) must be greater than 0 and less than the columns count ({ColumnsCount}).");
        }

        double[] columnComponents = new double[RowsCount];

        for (int i = 0; i < RowsCount; i++)
        {
            columnComponents[i] = _rows[i][index];
        }

        return new Vector(columnComponents);
    }

    public void Transpose()
    {
        Vector[] transposedRows = new Vector[ColumnsCount];

        for (int i = 0; i < ColumnsCount; i++)
        {
            transposedRows[i] = GetColumnByIndex(i);
        }

        _rows = transposedRows;
    }

    public void MultiplyByScalar(double scalar)
    {
        foreach (Vector row in _rows)
        {
            row.MultiplyByScalar(scalar);
        }
    }

    public double GetDeterminant()
    {
        if (!IsSquare)
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
            return matrix._rows[0][0];
        }

        if (size == 2)
        {
            return matrix._rows[0][0] * matrix._rows[1][1] - matrix._rows[0][1] * matrix._rows[1][0];
        }

        double determinant = 0;

        for (int expansionColumnIndex = 0; expansionColumnIndex < size; expansionColumnIndex++)
        {
            Matrix minor = GetMinor(matrix, expansionColumnIndex);
            double sign = (expansionColumnIndex % 2 == 0) ? 1 : -1;

            determinant += sign * matrix._rows[0][expansionColumnIndex] * GetDeterminant(minor);
        }

        return determinant;
    }

    private static Matrix GetMinor(Matrix matrix, int excludedColumnIndex)
    {
        int size = matrix.RowsCount;
        double[,] minor = new double[size - 1, size - 1];

        for (int i = 1; i < size; i++)
        {
            int minorColumnIndex = 0;

            for (int j = 0; j < size; j++)
            {
                if (j == excludedColumnIndex)
                {
                    continue;
                }

                minor[i - 1, minorColumnIndex] = matrix._rows[i][j];
                minorColumnIndex++;
            }
        }

        return new Matrix(minor);
    }

    public Vector MultiplyByVector(Vector vector)
    {
        if (vector.Size != ColumnsCount)
        {
            throw new ArgumentException($"The size ({vector.Size}) of the vector must be equal to the number of columns of the matrix.", nameof(vector));
        }

        Vector resultVector = new(RowsCount);

        for (int i = 0; i < resultVector.Size; i++)
        {
            resultVector[i] = Vector.GetDotProduct(_rows[i], vector);
        }

        return resultVector;
    }

    public void Add(Matrix matrix)
    {
        CheckSameSize(this, matrix);

        for (int i = 0; i < RowsCount; i++)
        {
            _rows[i].Add(matrix._rows[i]);
        }
    }

    public void Subtract(Matrix matrix)
    {
        CheckSameSize(this, matrix);

        for (int i = 0; i < RowsCount; i++)
        {
            _rows[i].Subtract(matrix._rows[i]);
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append('{');

        foreach (Vector row in _rows)
        {
            stringBuilder.Append(row).Append(", ");
        }

        stringBuilder.Length -= 2;
        stringBuilder.Append('}');

        return stringBuilder.ToString();
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

        if (_rows.Length != matrix.RowsCount || ColumnsCount != matrix.ColumnsCount)
        {
            return false;
        }

        for (int i = 0; i < _rows.Length; i++)
        {
            if (!_rows[i].Equals(matrix._rows[i]))
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

        foreach (Vector row in _rows)
        {
            hash = prime * hash + row.GetHashCode();
        }

        return hash;
    }
}
