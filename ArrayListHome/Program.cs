namespace ArrayListHome;

internal class Program
{
    static void Main(string[] args)
    {
        string filePath = Path.Combine("..", "..", "..", "someFile.txt");

        List<string> fileStrings = [];

        using (StreamReader reader = new(filePath))
        {
            while (!reader.EndOfStream)
            {
                fileStrings.Add(reader.ReadLine()!);
            }
        }

        Console.WriteLine(string.Join(Environment.NewLine, fileStrings));
        Console.WriteLine();

        List<int> numbers1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        for (int i = 0; i < numbers1.Count; i++)
        {
            if (numbers1[i] % 2 == 0)
            {
                numbers1.RemoveAt(i);
            }
        }

        Console.WriteLine(string.Join(Environment.NewLine, numbers1));
        Console.WriteLine();

        List<int> numbers2 = [1, 5, 2, 1, 3, 5];
        List<int> uniqueNumbers = [];

        foreach (int number in numbers2)
        {
            if (!uniqueNumbers.Contains(number))
            {
                uniqueNumbers.Add(number);
            }
        }

        Console.WriteLine(string.Join(Environment.NewLine, uniqueNumbers));
    }
}
