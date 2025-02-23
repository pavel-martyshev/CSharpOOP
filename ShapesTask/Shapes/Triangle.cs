using ShapesTask.Interfaces;

namespace ShapesTask.Shapes;

public class Triangle(double x1, double y1, double x2, double y2, double x3, double y3) : IShape
{
    public double X1 { get; set; } = x1;

    public double Y1 { get; set; } = y1;

    public double X2 { get; set; } = x2;

    public double Y2 { get; set; } = y2;

    public double X3 { get; set; } = x3;

    public double Y3 { get; set; } = y3;

    private static double GetSideLength(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }

    public double Side1 { get; } = GetSideLength(x2, x1, y2, y1);

    public double Side2 { get; } = GetSideLength(x3, x2, y3, y2);

    public double Side3 { get; } = GetSideLength(x1, x3, y1, y3);

    public double GetWidth() => Math.Max(X1, Math.Max(X2, X3)) - Math.Min(X1, Math.Min(X2, X3));

    public double GetHeight() => Math.Max(Y1, Math.Max(Y2, Y3)) - Math.Min(Y1, Math.Min(Y2, Y3));

    public double GetArea() => 0.5 * Math.Abs(X1 * (Y2 - Y3) + X2 * (Y3 - Y1) + X3 * (Y1 - Y2));

    public double GetPerimeter() => Side1 + Side2 + Side3;

    public override string ToString() => $"Треугольник(({X1}, {Y1}), ({X2}, {Y2}), ({X3}, {Y3}))";

    public override int GetHashCode()
    {
        const int prime = 17;
        int hash = 1;

        hash = prime * hash + X1.GetHashCode();
        hash = prime * hash + Y1.GetHashCode();

        hash = prime * hash + X2.GetHashCode();
        hash = prime * hash + Y2.GetHashCode();

        hash = prime * hash + X3.GetHashCode();
        hash = prime * hash + Y3.GetHashCode();

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

        Triangle triangle = (Triangle)obj;
        return X1 == triangle.X1 && Y1 == triangle.Y1 && X2 == triangle.X2 && Y2 == triangle.Y2 && X3 == triangle.X3 && Y3 == triangle.Y3;
    }
}
