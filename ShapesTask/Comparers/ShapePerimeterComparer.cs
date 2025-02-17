using ShapesTask.Interfaces;

namespace ShapesTask.Comparers;

internal class ShapePerimeterComparer : IComparer<IShape>
{
    public int Compare(IShape? shape1, IShape? shape2)
    {
        if (shape1 is null)
        {
            throw new ArgumentNullException(nameof(shape1), "One or both objects to compare are null.");
        }

        if (shape2 is null)
        {
            throw new ArgumentNullException(nameof(shape2), "One or both objects to compare are null.");
        }

        return shape1.GetPerimeter().CompareTo(shape2.GetPerimeter());
    }
}
