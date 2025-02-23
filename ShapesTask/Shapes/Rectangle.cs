using ShapesTask.Interfaces;

namespace ShapesTask.Shapes;

public class Rectangle(double height, double width) : IShape
{
    public double Height { get; set; } = height;

    public double Width { get; set; } = width;

    public double GetWidth() => Width;

    public double GetHeight() => Height;

    public double GetArea() => Height * Width;

    public double GetPerimeter() => 2 * (Height + Width);

    public override string ToString()
    {
        return $"Прямоугольник({Height}, {Width})";
    }

    public override int GetHashCode()
    {
        const int prime = 17;
        int hash = 1;

        hash = prime * hash + Height.GetHashCode();
        hash = prime * hash + Width.GetHashCode();

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
        return Height == rectangle.Height && Width == rectangle.Width;
    }
}
