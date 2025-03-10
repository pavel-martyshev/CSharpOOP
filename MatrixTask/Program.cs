using VectorTask;

namespace MatrixTask;

internal class Program
{
    static void Main(string[] args)
    {
        double[,] matrixArray = 
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        Matrix matrix1 = new(matrixArray);

        double[,] matrixArray2 = 
        {
            { 9, 8, 7 },
            { 6, 5, 4 },
            { 3, 2, 1 }
        };

        Matrix matrix2 = new(matrixArray2);

        matrix1.Add(matrix2);

        Console.WriteLine($"Матрица 1:{Environment.NewLine}{matrix1}");
        Console.WriteLine();
        Console.WriteLine($"Матрица 2:{Environment.NewLine}{matrix2}");

        Matrix matricesSum = Matrix.GetSum(matrix1, matrix2);
        Console.WriteLine($"{Environment.NewLine}Матрица 1 + Матрица 2:{Environment.NewLine}{matricesSum}");

        Matrix matricesSubtract = Matrix.GetDifference(matrix1, matrix2);
        Console.WriteLine($"{Environment.NewLine}Матрица 1 - Матрица 2:{Environment.NewLine}{matricesSubtract}");

        Matrix matricesMultiplication = Matrix.GetProduct(matrix1, matrix2);
        Console.WriteLine($"{Environment.NewLine}Матрица 1 * Матрица 2:{Environment.NewLine}{matricesMultiplication}");

        matrix1.Transpose();
        Console.WriteLine($"{Environment.NewLine}Транспонированная матрица 1:{Environment.NewLine}{matrix1}");

        matrix1.MultiplyByScalar(2);
        Console.WriteLine($"{Environment.NewLine}Матрица 1 после умножения на скаляр(2):{Environment.NewLine}{matrix1}");

        try
        {
            Console.WriteLine($"{Environment.NewLine}Определитель матрицы 1: {matrix1.GetDeterminant()}");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine();

        Vector vector = new([1, 2, 3]);
        Console.WriteLine($"Результат умножения матрицы 1 вектор ({vector}):{Environment.NewLine}{matrix1.MultiplyByVector(vector)}");
    }
}
