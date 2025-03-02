using ShapesTask.Comparers;
using ShapesTask.Interfaces;
using ShapesTask.Shapes;

namespace ShapesTask;

internal class Program
{
    static void Main(string[] args)
    {
        Random random = new();
        IShape[] shapes =
        [
            new Square(random.Next(10, 110)),
            new Triangle(random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200)),
            new Rectangle(random.Next(10, 110), random.Next(10, 110)),
            new Circle(random.Next(10, 60)),
            new Square(random.Next(10, 110)),
            new Triangle(random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200), random.Next(1, 200)),
            new Rectangle(random.Next(10, 110), random.Next(10, 110)),
            new Circle(random.Next(10, 60))
        ];

        //IShape[] shapes =
        //[
        //    new Square(10),                 // Площадь = 100, Периметр = 40
        //    new Triangle(0, 0, 4, 0, 4, 6), // Площадь = 12, Периметр = 17,21
        //    new Rectangle(5, 8),            // Площадь = 40, Периметр = 26
        //    new Circle(5),                  // Площадь ≈ 78.54, Периметр = 31,42
        //    new Square(7),                  // Площадь = 49, Периметр = 28
        //    new Triangle(0, 0, 6, 0, 3, 6), // Площадь = 9, Периметр = 19,42
        //    new Rectangle(4, 6),            // Площадь = 24, Периметр = 20
        //    new Circle(3)                   // Площадь ≈ 28.27, Периметр = 18,85
        //];

        Array.Sort(shapes, new ShapeAreaComparer());

        IShape maxAreaShape = shapes[^1];

        Console.WriteLine($"Фигура с самой большой площадью - {maxAreaShape}:");
        Console.WriteLine($"Площадь - {maxAreaShape.GetArea()}");
        Console.WriteLine($"Ширина - {maxAreaShape.GetWidth()}");
        Console.WriteLine($"Высота - {maxAreaShape.GetHeight()}");
        Console.WriteLine($"Периметр - {maxAreaShape.GetPerimeter()}{Environment.NewLine}");

        Array.Sort(shapes, new ShapePerimeterComparer());

        IShape secondPerimeterShape = shapes[^2];

        Console.WriteLine($"Фигура со вторым по величине периметром - {secondPerimeterShape}:");
        Console.WriteLine($"Площадь - {secondPerimeterShape.GetArea()}");
        Console.WriteLine($"Ширина - {secondPerimeterShape.GetWidth()}");
        Console.WriteLine($"Высота - {secondPerimeterShape.GetHeight()}");
        Console.WriteLine($"Периметр - {secondPerimeterShape.GetPerimeter()}{Environment.NewLine}");
    }
}
