namespace RangeTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите начало интервала:" + Environment.NewLine + "> ");
            double rangeStart = double.Parse(Console.ReadLine()!.Replace('.', ','));

            Console.Write("Введите конец интервала:" + Environment.NewLine + "> ");
            double rangeEnd = double.Parse(Console.ReadLine()!.Replace('.', ','));

            Range range = new(rangeStart, rangeEnd);

            Console.WriteLine($"Длина интервала: {range.GetLength():f2}");

            Console.Write("Введите число для проверки:" + Environment.NewLine + "> ");
            double numberToCheck = double.Parse(Console.ReadLine()!.Replace('.', ','));

            if (range.IsInside(numberToCheck))
            {
                Console.WriteLine($"Число {numberToCheck} находится внутри интервала от {range.From} до {range.To}");
            }
            else
            {
                Console.WriteLine($"Число {numberToCheck} находится вне интервала от {range.From} до {range.To}");
            }

            Console.Write("Введите начало второго интервала:" + Environment.NewLine + "> ");
            double range2Start = double.Parse(Console.ReadLine()!.Replace('.', ','));

            Console.Write("Введите конец интервала:" + Environment.NewLine + "> ");
            double range2End = double.Parse(Console.ReadLine()!.Replace('.', ','));

            Range range2 = new(range2Start, range2End);
            Range? intersection = range.GetIntersection(range2);

            if (intersection is not null)
            {
                Console.WriteLine($"Интервалы {range} и {range2} пересекаются в точках {intersection}");
            }
            else
            {
                Console.WriteLine($"Интервалы {range} и {range2} не пересекаются");
            }

            Console.WriteLine($"Результат объединения интервалов {range} и {range2}:");

            foreach (Range interval in range.UnionIntervals(range2))
            {
                Console.WriteLine(interval);
            }

            Range[]? intervalsDifference = range.GetIntervalsDifference(range2);

            if (intervalsDifference is not null)
            {
                Console.WriteLine($"Разность интервалов {range} и {range2}:");

                foreach (Range difference in intervalsDifference)
                {
                    Console.WriteLine(difference);
                }
            }
            else
            {
                Console.WriteLine($"Не удалось найти разность интервалов {range} и {range2}");
            }
        }
    }
}
