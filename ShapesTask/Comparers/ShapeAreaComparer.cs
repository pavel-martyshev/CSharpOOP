using ShapesTask.Interfaces;

namespace ShapesTask.Comparers;

internal class ShapeAreaComparer : IComparer<IShape>
{
    public int Compare(IShape? shape1, IShape? shape2)
    {
        ArgumentNullException.ThrowIfNull(shape1);
        ArgumentNullException.ThrowIfNull(shape2);

        return shape1.GetArea().CompareTo(shape2.GetArea());
    }
}
