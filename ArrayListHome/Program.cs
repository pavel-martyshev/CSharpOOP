namespace ArrayListHome;

internal class Program
{
    public static List<string> GetFileLinesAsList(string filePath)
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

    public static void RemoveEvenNumbers(List<int> numbers)
    {
        if (numbers.Count == 0)
        {
            throw new ArgumentException("Список не должен быть пустым.", nameof(numbers));
        }

        for (int i = numbers.Count - 1; i >= 0; i--)
        {
            if (numbers[i] % 2 == 0)
            {
                numbers.RemoveAt(i);
            }
        }
    }

    public static List<T> GetUniqueElements<T>(List<T> list)
    {
        if (list.Count == 0)
        {
            throw new ArgumentException("Список не должен быть пустым.", nameof(list));
        }

        List<T> uniqueElements = new(list.Count);

        foreach (T element in list)
        {
            if (!uniqueElements.Contains(element))
            {
                uniqueElements.Add(element);
            }
        }

        return uniqueElements;
    }

    static void Main(string[] args)
    {
        string filePath = Path.Combine("..", "..", "..", "someFile.txt");

        try
        {
            Console.WriteLine(string.Join(Environment.NewLine, GetFileLinesAsList(filePath)));
        }
        catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
        {
            Console.WriteLine($"Файл не найден ({filePath})");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Нет прав для чтения файла ({filePath})");
        }

        Console.WriteLine();

        List<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        try
        {
            RemoveEvenNumbers(numbers);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine(string.Join(Environment.NewLine, numbers));

        Console.WriteLine();

        try
        {
            Console.WriteLine(string.Join(Environment.NewLine, GetUniqueElements([1, 5, 2, 1, 3, 5])));
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
