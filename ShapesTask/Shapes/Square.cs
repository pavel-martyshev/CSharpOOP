using ShapesTask.Interfaces;

namespace ShapesTask.Shapes;

internal class Square(double sideLength) : IShape
{
    private double SideLength { get; set; } = sideLength;

    public double GetWidth() => SideLength;

    public double GetHeight() => SideLength;

    public double GetArea() => SideLength * SideLength;

    public double GetPerimeter() => SideLength * 4;

    public override string ToString()
    {
        return $"Квадрат({SideLength})";
    }

    public override int GetHashCode() => 17 + SideLength.GetHashCode();

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
        return SideLength == square.SideLength;
    }
}
