using ShapesTask.Interfaces;

namespace ShapesTask.Shapes;

internal class Square(double a) : IShape
{
    private double A { get; set; } = a;

    public double GetWidth() => A;

    public double GetHeight() => 0;

    public double GetArea() => A * A;

    public double GetPerimeter() => A * 4;

    public override string ToString()
    {
        return $"Квадрат({A})";
    }

    public override int GetHashCode() => 17 * 1 + A.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        Square square = (Square)obj;
        return A == square.A;
    }
}
