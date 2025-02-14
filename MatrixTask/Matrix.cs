using VectorTask;

namespace MatrixTask;

class Matrix
{
    public List<Vector> Elements { get; set; }

    public static Matrix AdditionTwoMatrices(Matrix matrix1, Matrix matrix2)
    {
        Matrix matrix = new(matrix1);
        matrix.AddMatrix(matrix2);

        return matrix;
    }

    public static Matrix SubtractTwoMatrices(Matrix matrix1, Matrix matrix2)
    {
        Matrix matrix = new(matrix1);
        matrix.SubtractMatrix(matrix2);

        return matrix;
    }

    public static Matrix MultiplyTwoMatrices(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.GetColumnsCount() != matrix2.GetRowsCount())
        {
            throw new ArgumentException("The number of columns of the first matrix must be equal to the number of rows of the second matrix.");
        }

        Matrix multiplicationProduct = new(matrix1.GetRowsCount(), matrix2.GetColumnsCount());

        for (int i = 0; i < matrix1.GetRowsCount(); i++)
        {
            for (int j = 0; j < matrix2.GetColumnsCount(); j++)
            {
                multiplicationProduct[i][j] = Vector.GetDotProduct(matrix1[i], matrix2.GetColumnByIndex(j));
            }
        }

        return multiplicationProduct;
    }

    public Matrix(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
        {
            string argumentName = rows <= 0 ? nameof(rows) : nameof(columns);
            throw new ArgumentException("The argument must be greater than 0.", argumentName);
        }

        Elements = new List<Vector>(rows);

        for (int i = 0; i < rows; i++)
        {
            Elements.Add(new Vector(columns));
        }
    }

    public Matrix(Matrix matrix)
    {
        Elements = new List<Vector>(matrix.GetRowsCount());

        for (int i = 0; i < matrix.GetRowsCount(); i++)
        {
            Elements.Add(new Vector(matrix[i]));
        }        
    }

    public Matrix(double[,] matrixArray)
    {
        Elements = new List<Vector>(matrixArray.GetLength(0));

        for (int i = 0; i < matrixArray.GetLength(0); i++)
        {
            Vector vector = new(matrixArray.GetLength(1));

            for (int j = 0; j < matrixArray.GetLength(1); j++)
            {
                vector[j] = matrixArray[i, j];
            }

            Elements.Add(vector);
        }
    }

    public Matrix(Vector[] vectorsArray)
    {
        Elements = [.. vectorsArray];

        int maxVectorsSize = vectorsArray.Aggregate(vectorsArray[0].GetSize(), (maxSize, vector) => Math.Max(maxSize, vector.GetSize()));

        for (int i = 0; i < Elements.Count; i++)
        {
            Vector vector = Elements[i];

            if (vector.GetSize() < maxVectorsSize)
            {
                Elements[i] = new Vector([.. vector.Components, .. new double[maxVectorsSize - vector.GetSize()]]);
            }
        }
    }

    public Vector this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentException("index must be greater than 0.", nameof(index));
            }

            return Elements[index];
        }
        set
        {
            if (index < 0)
            {
                throw new ArgumentException("index must be greater than 0.", nameof(index));
            }

            Elements[index] = value;
        }
    }

    public Vector GetColumnByIndex(int index)
    {
        if (index < 0)
        {
            throw new ArgumentException("Argument must be greater than or equal to 0.", nameof(index));
        }

        List<double> columnComponents = [];

        foreach (Vector row in Elements)
        {
            columnComponents.Add(row[index]);
        }

        return new Vector([.. columnComponents]);
    }

    public int GetRowsCount() => Elements.Count;

    public int GetColumnsCount() => Elements[0].GetSize();

    public bool IsSquareMatrix() => GetRowsCount() == GetColumnsCount();

    public string GetSize() => $"{GetRowsCount}x{GetColumnsCount}";

    public void Transpose()
    {
        List<Vector> transposedMatrixElements = [];

        for (int i = 0; i < GetColumnsCount(); i++)
        {
            transposedMatrixElements.Add(GetColumnByIndex(i));
        }

        Elements = [.. transposedMatrixElements];
    }

    public void MultiplyByScalar(double scalar) => Elements.ForEach(row => row.MultiplyByScalar(scalar));

    public double GetDeterminant()
    {
        if (!IsSquareMatrix())
        {
            throw new InvalidOperationException("Cannot compute the determinant of a non-square matrix.");
        }

        return GetDeterminant(this);
    }

    private static double GetDeterminant(Matrix matrix)
    {
        int size = matrix.GetRowsCount();

        if (size == 1)
        {
            return matrix[0][0];
        }

        if (size == 2)
        {
            return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
        }

        double determinant = 0;

        for (int col = 0; col < size; col++)
        {
            Matrix subMatrix = GetMinor(matrix, col);
            double sign = (col % 2 == 0) ? 1 : -1;

            determinant += sign * matrix[0][col] * GetDeterminant(subMatrix);
        }

        return determinant;
    }

    private static Matrix GetMinor(Matrix matrix, int excludedColumn)
    {
        int size = matrix.GetRowsCount();
        double[,] minor = new double[size - 1, size - 1];

        for (int i = 1; i < size; i++)
        {
            int minorColumn = 0;

            for (int j = 0; j < size; j++)
            {
                if (j == excludedColumn)
                {
                    continue;
                }

                minor[i - 1, minorColumn] = matrix[i][j];
                minorColumn++;
            }
        }

        return new Matrix(minor);
    }

    public Vector MultiplyByVector(Vector vector)
    {
        if (vector.GetSize() != GetColumnsCount())
        {
            throw new ArgumentException("The vector size must be equal to the matrix column size.");
        }

        Vector resultVector = new(vector);

        for (int i = 0; i < GetRowsCount(); i++)
        {
            double component = 0;

            for (int j = 0; j < GetColumnsCount(); j++)
            {
                component += this[i][j] * vector[j];
            }

            resultVector[i] = component;
        }

        return resultVector;
    }

    public void AddMatrix(Matrix matrix)
    {
        if (matrix.GetRowsCount() != GetRowsCount() || matrix.GetColumnsCount() != GetColumnsCount())
        {
            throw new ArgumentException("The matrices must be the same size.");
        }

        for (int i = 0; i < GetRowsCount(); i++)
        {
            for (int j = 0; j < GetColumnsCount(); j++)
            {
                this[i][j] += matrix[i][j];
            }
        }
    }

    public void SubtractMatrix(Matrix matrix)
    {
        if (matrix.GetRowsCount() != GetRowsCount() || matrix.GetColumnsCount() != GetColumnsCount())
        {
            throw new ArgumentException("The matrices must be the same size.");
        }

        for (int i = 0; i < GetRowsCount(); i++)
        {
            for (int j = 0; j < GetColumnsCount(); j++)
            {
                this[i][j] -= matrix[i][j];
            }
        }
    }

    public override string ToString() => $"{string.Join(Environment.NewLine, Elements)}";
}
