using ShapesTask.Interfaces;

namespace ShapesTask.Shapes;

internal class Triangle(double x1, double y1, double x2, double y2, double x3, double y3) : IShape
{
    public double X1 { get; set; } = x1;

    public double Y1 { get; set; } = y1;

    public double X2 { get; set; } = x2;

    public double Y2 { get; set; } = y2;

    public double X3 { get; set; } = x3;

    public double Y3 { get; set; } = y3;

    public double Side1 { get; set; } = Math.Round(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)), 2, MidpointRounding.AwayFromZero);

    public double Side2 { get; set; } = Math.Round(Math.Sqrt(Math.Pow(x3 - x2, 2) + Math.Pow(y3 - y2, 2)), 2, MidpointRounding.AwayFromZero);

    public double Side3 { get; set; } = Math.Round(Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2)), 2, MidpointRounding.AwayFromZero);

    private static double GetMaxCoordinate(double point1, double point2, double point3)
    {
        if (point1 >= point2 && point1 >= point3)
        {
            return point1;
        }

        if (point2 >= point1 && point2 >= point3)
        {
            return point2;
        }

        return point3;
    }

    private static double GetMinCoordinate(double point1, double point2, double point3)
    {
        if (point1 <= point2 && point1 <= point3)
        {
            return point1;
        }

        if (point2 <= point1 && point2 <= point3)
        {
            return point2;
        }

        return point3;
    }

    public double GetWidth() => GetMaxCoordinate(X1, X2, X3) - GetMinCoordinate(X1, X2, X3);

    public double GetHeight() => GetMaxCoordinate(Y1, Y2, Y3) - GetMinCoordinate(Y1, Y2, Y3);

    public double GetArea() => 0.5 * Math.Abs(X1 * (Y2 - Y3) + X2 * (Y3 - Y1) + X3 * (Y1 - Y2));

    public double GetPerimeter() => Math.Round(Side1 + Side2 + Side3, 2, MidpointRounding.AwayFromZero);

    public override string ToString() => $"Треугольник(({X1}, {Y1}), ({X2}, {Y2}), ({X3}, {Y3}))";

    public override int GetHashCode()
    {
        int prime = 17;
        int hash = 1;

        hash = prime * hash + X1.GetHashCode();
        hash = prime * hash + Y1.GetHashCode();

        hash = prime * hash + X2.GetHashCode();
        hash = prime * hash + Y2.GetHashCode();

        hash = prime * hash + X3.GetHashCode();
        hash = prime * hash + Y3.GetHashCode();

        hash = prime * hash + Side1.GetHashCode();
        hash = prime * hash + Side2.GetHashCode();
        hash = prime * hash + Side3.GetHashCode();

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
        return Side1 == triangle.Side1 && Side2 == triangle.Side2 && Side3 == triangle.Side3;
    }
}
