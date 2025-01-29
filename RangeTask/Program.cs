namespace RangeTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double rangeStart = 1;
            double rangeEnd = 5;

            Range range = new(rangeStart, rangeEnd);

            //Console.Write("Введите начало диапазона:" + Environment.NewLine + "> ");
            //double rangeStart = double.Parse(Console.ReadLine()!.Replace('.', ','));

            //Console.Write("Введите конец диапазона:" + Environment.NewLine + "> ");
            //double rangeEnd = double.Parse(Console.ReadLine()!.Replace('.', ','));

            //Range range = new(rangeStart, rangeEnd);

            //Console.WriteLine($"Длина диапазона: {range.GetLength():f2}");

            //Console.Write("Введите число для проверки:" + Environment.NewLine + "> ");
            //double numberToCheck = double.Parse(Console.ReadLine()!.Replace('.', ','));

            //if (range.IsInside(numberToCheck))
            //{
            //    Console.WriteLine($"Число {numberToCheck} находится внутри диапазона от {range.From} до {range.To}");
            //}
            //else
            //{
            //    Console.WriteLine($"Число {numberToCheck} находится вне диапазона от {range.From} до {range.To}");
            //}
        }
    }
}
