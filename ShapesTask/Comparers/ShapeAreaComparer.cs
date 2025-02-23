using ShapesTask.Interfaces;

namespace ShapesTask.Comparers;

internal class ShapeAreaComparer : IComparer<IShape>
{
    public int Compare(IShape? shape1, IShape? shape2)
    {
        if (shape1 is null)
        {
            throw new ArgumentNullException(nameof(shape1), "The object must not be equal to null.");
        }

        if (shape2 is null)
        {
            throw new ArgumentNullException(nameof(shape2), "The object must not be equal to null.");
        }

        return shape1.GetArea().CompareTo(shape2.GetArea());
    }
}
