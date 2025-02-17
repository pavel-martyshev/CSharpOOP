namespace RangeTask;

internal class Range(double from, double to)
{
    public double From { get; set; } = from;

    public double To { get; set; } = to;

    public override string ToString()
    {
        return $"({From}, {To})";
    }

    public double GetLength()
    {
        return To - From;
    }

    public bool IsInside(double number)
    {
        return number >= From && number <= To;
    }

    public Range? GetIntersection(Range range)
    {
        double intersectionFrom = Math.Max(From, range.From);
        double intersectionTo = Math.Min(To, range.To);

        if (intersectionFrom < intersectionTo && intersectionFrom != intersectionTo)
        {
            return new Range(intersectionFrom, intersectionTo);
        }

        return null;
    }

    public Range[] GetUnion(Range range)
    {
        if (IsInside(range.From) || IsInside(range.To))
        {
            return [new Range(Math.Min(From, range.From), Math.Max(To, range.To))];
        }

        return [new Range(Math.Min(From, range.From), Math.Min(To, range.To)), new Range(Math.Max(From, range.From), Math.Max(To, range.To))];
    }

    public Range[] GetDifference(Range range)
    {
        if (IsInside(range.From) && IsInside(range.To))
        {
            if (To == range.To)
            {
                return [new Range(From, range.From)];
            }

            return [new Range(From, range.From), new Range(range.To, To)];
        }

        return [];
    }
}
