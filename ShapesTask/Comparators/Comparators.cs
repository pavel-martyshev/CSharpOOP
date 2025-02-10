using ShapesTask.Interfaces;

namespace ShapesTask.Comparators;

internal class IShapeAreaComparer : IComparer<IShape>
{
    public int Compare(IShape? shape1, IShape? shape2)
    {
        if (shape1 == null || shape2 == null)
        {
            throw new ArgumentException("One or both objects to compare are null.");
        }

        return shape1.GetArea().CompareTo(shape2.GetArea());
    }
}

internal class IShapePerimeterComparer : IComparer<IShape>
{
    public int Compare(IShape? shape1, IShape? shape2)
    {
        if (shape1 == null || shape2 == null)
        {
            throw new ArgumentException("One or both objects to compare are null.");
        }

        return shape1.GetPerimeter().CompareTo(shape2.GetPerimeter());
    }
}
