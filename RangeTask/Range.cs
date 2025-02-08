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

        if (To < intersectionFrom | intersectionFrom == intersectionTo)
        {
            return null;
        }

        return new Range(intersectionFrom, intersectionTo);
    }

    public Range[] GetUnion(Range range)
    {
        if (To >= range.From)
        {
            return [new Range(Math.Min(From, range.From), Math.Max(To, range.To))];
        }

        return [this, range];
    }

    public Range[] GetDifference(Range range)
    {
        if ((GetIntersection(range) is null) | (From >= range.From && To <= range.To))
        {
            return [new Range(0, 0)];
        }

        List<Range> ranges = [];

        if (From < range.From)
        {
            ranges.Add(new Range(From, Math.Min(To, range.From - 1)));
        }

        if (To > range.To)
        {
            ranges.Add(new Range(Math.Max(From, range.To + 1), To));
        }

        return [.. ranges];
    }
}
