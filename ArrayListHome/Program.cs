namespace ArrayListHome;

internal class Program
{
    public static List<string> GetFileLineAsList(string filePath)
    {
        using StreamReader reader = new(filePath);
        string? line = reader.ReadLine();

        List<string> fileLines = [];

        while (line is not null)
        {
            fileLines.Add(line);
            line = reader.ReadLine()!;
        }

        return fileLines;
    }

    public static List<int> RemoveEvenNumbers(List<int> numbers)
    {
        for (int i = numbers.Count - 1; i >= 0; i--)
        {
            if (numbers[i] % 2 == 0)
            {
                numbers.RemoveAt(i);
            }
        }

        return numbers;
    }

    public static List<int> ExtractUniqueElements(List<int> numbers)
    {
        List<int> uniqueNumbers = new(numbers.Count);

        foreach (int number in numbers)
        {
            if (!uniqueNumbers.Contains(number))
            {
                uniqueNumbers.Add(number);
            }
        }

        return uniqueNumbers;
    }

    static void Main(string[] args)
    {
        string filePath = Path.Combine("..", "..", "..", "someFile.txt");

        try
        {
            Console.WriteLine(string.Join(Environment.NewLine, GetFileLineAsList(filePath)));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл не найден ({filePath})");
        }

        Console.WriteLine();

        Console.WriteLine(string.Join(Environment.NewLine, RemoveEvenNumbers([1, 2, 3, 4, 5, 6, 7, 8, 9, 10])));

        Console.WriteLine();

        Console.WriteLine(string.Join(Environment.NewLine, ExtractUniqueElements([1, 5, 2, 1, 3, 5])));
    }
}
