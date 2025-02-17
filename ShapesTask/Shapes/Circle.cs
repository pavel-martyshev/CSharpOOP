using ShapesTask.Interfaces;

namespace ShapesTask.Shapes;

internal class Circle(double radius) : IShape
{
    public double Radius { get; set; } = radius;

    public double GetWidth() => 2 * Radius;

    public double GetHeight() => 2 * Radius;

    public double GetArea() => Math.PI * (Radius * Radius);

    public double GetPerimeter() => 2 * Math.PI * Radius;

    public override string ToString()
    {
        return $"Круг({Radius})";
    }

    public override int GetHashCode() => 17 + Radius.GetHashCode();

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

        Circle circle = (Circle)obj;
        return Radius == circle.Radius;
    }
}
