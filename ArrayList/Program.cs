namespace ArrayList;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = [1, 2, 3];

        numbers.Insert(3, 4);

        Console.WriteLine($"Numbers: {numbers}");
        Console.WriteLine($"Its count: {numbers.Count}");

        numbers.Remove(2);

        Console.WriteLine($"Numbers after Remove(): {numbers}");
        Console.WriteLine($"Its count: {numbers.Count}");
        Console.WriteLine();

        foreach (int item in numbers)
        {
            Console.WriteLine(item);
        }
    }
}
