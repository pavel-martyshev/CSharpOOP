using ShapesTask.Comparers;
using ShapesTask.Interfaces;
using ShapesTask.Shapes;

namespace ShapesTask;

internal class Program
{
    static void Main(string[] args)
    {
        //Random random = new();
        //IShape[] shapes =
        //[
        //    new Square(random.Next(10, 110)),
        //    new Triangle(random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200)),
        //    new Rectangle(random.Next(10, 110), random.Next(10, 110)),
        //    new Circle(random.Next(10, 60)),
        //    new Square(random.Next(10, 110)),
        //    new Triangle(random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200)),
        //    new Rectangle(random.Next(10, 110), random.Next(10, 110)),
        //    new Circle(random.Next(10, 60))
        //];

        IShape[] shapes =
        [
            new Square(10),                 // Площадь = 100, Периметр = 40
            new Triangle(0, 0, 4, 0, 4, 6), // Площадь = 12, Периметр = 17,21
            new Rectangle(5, 8),            // Площадь = 40, Периметр = 26
            new Circle(5),                  // Площадь ≈ 78.54, Периметр = 31,42
            new Square(7),                  // Площадь = 49, Периметр = 28
            new Triangle(0, 0, 6, 0, 3, 6), // Площадь = 9, Периметр = 19,42
            new Rectangle(4, 6),            // Площадь = 24, Периметр = 20
            new Circle(3)                   // Площадь ≈ 28.27, Периметр = 18,85
        ];

        Array.Sort(shapes, new IShapeAreaComparer());

        Console.WriteLine($"Фигура с самой большой площадью - {shapes[^1]}:");
        Console.WriteLine($"Площадь - {shapes[^1].GetArea()}");
        Console.WriteLine($"Ширина - {shapes[^1].GetWidth()}");
        Console.WriteLine($"Высота - {shapes[^1].GetHeight()}");
        Console.WriteLine($"Периметр - {shapes[^1].GetPerimeter()}{Environment.NewLine}");

        Array.Sort(shapes, new IShapePerimeterComparer());

        Console.WriteLine($"Фигура со вторым по величине периметром - {shapes[^2]}:");
        Console.WriteLine($"Площадь - {shapes[^2].GetArea()}");
        Console.WriteLine($"Ширина - {shapes[^2].GetWidth()}");
        Console.WriteLine($"Высота - {shapes[^2].GetHeight()}");
        Console.WriteLine($"Периметр - {shapes[^2].GetPerimeter()}{Environment.NewLine}");
    }
}
