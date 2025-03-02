using System.Security.Cryptography.X509Certificates;

namespace ArrayListHome;

internal class Program
{
    public static void ReadingFile()
    {
        string filePath = Path.Combine("..", "..", "..", "someFile.txt");

        List<string> fileLines = [];

        try
        {
            using (StreamReader reader = new(filePath))
            {
                string line = reader.ReadLine()!;

                while (line is not null)
                {
                    fileLines.Add(reader.ReadLine()!);
                    line = reader.ReadLine()!;
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, fileLines));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл не найден ({filePath})");
        }
    }
    
    public static void RemoveEvenNumbers()
    {
        List<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        for (int i = numbers.Count - 1; i >= 0; i--)
        {
            if (numbers[i] % 2 == 0)
            {
                numbers.RemoveAt(i);
            }
        }

        Console.WriteLine(string.Join(Environment.NewLine, numbers));
    }

    public static void ExtractUniqueElements()
    {
        List<int> numbers = [1, 5, 2, 1, 3, 5];
        List<int> uniqueNumbers = new(numbers.Count);

        foreach (int number in numbers)
        {
            if (!uniqueNumbers.Contains(number))
            {
                uniqueNumbers.Add(number);
            }
        }

        Console.WriteLine(string.Join(Environment.NewLine, uniqueNumbers));
    }

    static void Main(string[] args)
    {
        ReadingFile();

        Console.WriteLine();

        RemoveEvenNumbers();

        Console.WriteLine();

        ExtractUniqueElements();
    }
}
