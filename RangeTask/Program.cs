namespace RangeTask;

internal class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите начало интервала:" + Environment.NewLine + "> ");
        double range1From = double.Parse(Console.ReadLine()!.Replace('.', ','));

        Console.Write("Введите конец интервала:" + Environment.NewLine + "> ");
        double range1To = double.Parse(Console.ReadLine()!.Replace('.', ','));

        Range range1 = new(range1From, range1To);

        Console.WriteLine($"Длина интервала: {range1.GetLength():f2}");

        Console.Write("Введите число для проверки:" + Environment.NewLine + "> ");
        double numberToCheck = double.Parse(Console.ReadLine()!.Replace('.', ','));

        if (range1.IsInside(numberToCheck))
        {
            Console.WriteLine($"Число {numberToCheck} находится внутри интервала от {range1.From} до {range1.To}");
        }
        else
        {
            Console.WriteLine($"Число {numberToCheck} находится вне интервала от {range1.From} до {range1.To}");
        }

        Console.Write("Введите начало второго интервала:" + Environment.NewLine + "> ");
        double range2From = double.Parse(Console.ReadLine()!.Replace('.', ','));

        Console.Write("Введите конец интервала:" + Environment.NewLine + "> ");
        double range2To = double.Parse(Console.ReadLine()!.Replace('.', ','));

        Range range2 = new(range2From, range2To);

        Range? intersection = range1.GetIntersection(range2);

        if (intersection is not null)
        {
            Console.WriteLine($"Интервалы {range1} и {range2} пересекаются в точках {intersection}");
        }
        else
        {
            Console.WriteLine($"Интервалы {range1} и {range2} не пересекаются");
        }

        Console.WriteLine($"Результат объединения интервалов {range1} и {range2}:");
        Console.WriteLine(string.Join(Environment.NewLine, (object?[])range1.GetUnion(range2)));

        Range[] difference = range1.GetDifference(range2);
        Console.WriteLine($"Разность интервалов {range1} и {range2}:");

        if (difference.Length == 0)
        {
            Console.WriteLine(0);
        }
        else
        {
            Console.WriteLine(string.Join(Environment.NewLine, (object?[])difference));
        }
    }
}
