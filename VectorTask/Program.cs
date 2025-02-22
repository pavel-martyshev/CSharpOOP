namespace VectorTask;

internal class Program
{
    static void Main(string[] args)
    {
        Vector vector1 = new(6);
        vector1[0] = 1;
        vector1[1] = 2;
        vector1[2] = 3;

        Console.WriteLine($"Вектор 1: {vector1}");
        Console.WriteLine($"Длина вектора 1: {vector1.GetLength()}");

        Console.WriteLine();

        Vector vector2 = new([4, 5, 6, 7, 8, 9]);

        Console.WriteLine($"Вектор 2: {vector2}");
        Console.WriteLine($"Длина вектора 2: {vector2.GetLength()}");

        Console.WriteLine();

        Vector vector3 = Vector.GetVectorsAddition(vector1, vector2);
        Console.WriteLine($"Результат сложения вектора 1 и вектора 2: {vector3}");

        Console.WriteLine();

        Vector vector4 = Vector.GetVectorsSubtraction(vector2, vector1);
        Console.WriteLine($"Результат вычитания вектора 1 из вектора 2: {vector4}");

        Console.WriteLine();

        double dotProduct = Vector.GetDotProduct(vector1, vector2);
        Console.WriteLine($"Скалярное произведение вектора 1 и вектора 2: {dotProduct}");
    }
}
