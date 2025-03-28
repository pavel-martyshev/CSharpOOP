namespace Lambdas;

using System.Linq;

internal class Program
{
    public static List<string> GetUniqueName(List<Person> people)
    {
        return [.. people.Select(p => p.Name).Distinct()];
    }

    public static List<Person> GetPeopleUnder18(List<Person> people)
    {
        return [.. people.Where(p => p.Age < 18)];
    }

    public static double GetAverageAge(List<Person> people)
    {
        return (double)people.Sum(p => p.Age) / people.Count;
    }

    public static Dictionary<string, double> GetNameToAverageAgeMap(List<Person> people)
    {
        return people
            .GroupBy(p => p.Name)
            .ToDictionary(p => p.Key, p => (double)p
                .Sum(p => p.Age) / p
                .ToList().Count);
    }

    public static List<Person> GetPeopleInAgeRangeDescending(List<Person> people, int minAge, int maxAge)
    {
        return [.. people
            .Where(p => p.Age >= minAge && p.Age <= maxAge)
            .OrderByDescending(p => p.Age)];
    }

    public static IEnumerable<long> GetFibonacciSequenceElement(int maxElementsCount)
    {
        int i = 0;
        long fibonacciNumber = 0;
        long nextFibonacciNumber = 1;

        while (i < maxElementsCount)
        {
            long previousFibonacciNumber = fibonacciNumber;
            fibonacciNumber = nextFibonacciNumber;
            nextFibonacciNumber = fibonacciNumber + previousFibonacciNumber;

            yield return fibonacciNumber;

            i++;
        }
    }

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Task 1: Handle Person list");
        Console.ResetColor();
        Console.WriteLine();

        List<Person> people =
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

        Console.WriteLine($"Names: {string.Join(", ", GetUniqueName(people))}");

        Console.WriteLine();

        var peopleUnder18 = GetPeopleUnder18(people);

        Console.WriteLine($"People under 18 years old: {string.Join(", ", peopleUnder18.Select(p => p.Name))}");
        Console.WriteLine($"The average age of people is under 18 years old: {GetAverageAge(peopleUnder18)}");

        Console.WriteLine();

        Console.WriteLine($"Names to average age:");

        foreach (var person in GetNameToAverageAgeMap(people))
        {
            Console.WriteLine($"Name = {person.Key}, average age = {person.Value}");
        }

        Console.WriteLine();

        Console.WriteLine($"People in the age range from 20 to 45 in descending order:");

        foreach (var person in GetPeopleInAgeRangeDescending(people, 20, 45))
        {
            Console.WriteLine(person);
        }

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Task 2: Fibonacci number stream");
        Console.ResetColor();
        Console.WriteLine();

        Console.Write($"Enter the count of elements in the sequence to print{Environment.NewLine}> ");
        var elementsCount = int.Parse(Console.ReadLine()!);

        foreach (var element in GetFibonacciSequenceElement(elementsCount))
        {
            Console.WriteLine(element);
        }
    }
}
