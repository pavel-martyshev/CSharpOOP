using ShapesTask.Interfaces;

namespace ShapesTask.Shapes;

internal class Rectangle(double a, double b) : IShape
{
    public double A { get; set; } = a;

    public double B { get; set; } = b;

    public double GetWidth() => Math.Min(A, B);

    public double GetHeight() => 0;

    public double GetArea() => A * B;

    public double GetPerimeter() => 2 * (A + B);

    public override string ToString()
    {
        return $"Прямоугольник({A}, {B})";
    }

    public override int GetHashCode()
    {
        int prime = 17;
        int hash = 1;

        hash = prime * hash + A.GetHashCode();
        hash = prime * hash + B.GetHashCode();

        return hash;
    }

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

        Rectangle rectangle = (Rectangle)obj;
        return A == rectangle.A && B == rectangle.B;
    }
}
