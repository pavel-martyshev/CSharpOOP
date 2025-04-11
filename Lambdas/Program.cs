namespace Lambdas;

using System.Linq;

internal class Program
{
    public static List<string> GetUniqueNames(List<Person> persons)
    {
        return persons.Select(p => p.Name).Distinct().ToList();
    }

    public static List<Person> GetPeopleUnder18(List<Person> persons)
    {
        return persons.Where(p => p.Age < 18).ToList();
    }

    public static double GetAverageAge(List<Person> persons)
    {
        return persons.Average(p => p.Age);
    }

    public static Dictionary<string, double> GetNameToAverageAgeMap(List<Person> persons)
    {
        return persons
            .GroupBy(p => p.Name)
            .ToDictionary(g => g.Key, g => g.Average(p => p.Age));
    }

    public static List<Person> GetPersonsInAgeRangeDescending(List<Person> persons, int minAge, int maxAge)
    {
        return persons
            .Where(p => p.Age >= minAge && p.Age <= maxAge)
            .OrderByDescending(p => p.Age)
            .ToList();
    }

    public static IEnumerable<long> GetFibonacciNumbersSequence()
    {
        long fibonacciNumber = 0;
        long nextFibonacciNumber = 1;

        while (true)
        {
            long previousFibonacciNumber = fibonacciNumber;
            fibonacciNumber = nextFibonacciNumber;
            nextFibonacciNumber = previousFibonacciNumber + fibonacciNumber;

            yield return fibonacciNumber;
        }
    }

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Task 1: Handle Person list");
        Console.ResetColor();
        Console.WriteLine();

        List<Person> persons =
        [
            new Person("Ivan", 15),
            new Person("Oleg", 18),
            new Person("Anna", 47),
            new Person("Maria", 16),
            new Person("Alex", 25),
            new Person("Dmitry", 54),
            new Person("Oleg", 19),
            new Person("Sergey", 61),
            new Person("Olga", 28),
            new Person("Pavel", 42),
            new Person("Alex", 11),
            new Person("Nikolay", 68),
            new Person("Svetlana", 23),
            new Person("Andrey", 51),
            new Person("Olga", 29)
        ];

        Console.WriteLine($"Names: {string.Join(", ", GetUniqueNames(persons))}");

        Console.WriteLine();

        var personsUnder18 = GetPeopleUnder18(persons);

        Console.WriteLine($"People under 18 years old: {string.Join(", ", personsUnder18.Select(p => p.Name))}");
        Console.WriteLine($"The average age of people is under 18 years old: {GetAverageAge(personsUnder18)}");

        Console.WriteLine();

        Console.WriteLine("Names to average age:");

        foreach (var dictItems in GetNameToAverageAgeMap(persons))
        {
            Console.WriteLine($"Name = {dictItems.Key}, average age = {dictItems.Value}");
        }

        Console.WriteLine();

        Console.WriteLine("People in the age range from 20 to 45 in descending order:");

        foreach (var person in GetPersonsInAgeRangeDescending(persons, 20, 45))
        {
            Console.WriteLine(person);
        }

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Task 2: Fibonacci number stream");
        Console.ResetColor();
        Console.WriteLine();

        Console.Write($"Enter the count of elements in the sequence to print{Environment.NewLine}> ");
        var fibonacciNumbersCount = int.Parse(Console.ReadLine()!);

        foreach (var number in GetFibonacciNumbersSequence().Take(fibonacciNumbersCount))
        {
            Console.WriteLine(number);
        }
    }
}
