namespace RangeTask
{
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
            if (From <= range.To && range.From <= To)
            {
                double startIntersection = Math.Max(From, range.From);
                double endIntersection = Math.Min(To, range.To);

                if (startIntersection != endIntersection)
                {
                    return new Range(startIntersection, endIntersection);
                }

                return null;
            }

            return null;
        }

        public Range[] UnionIntervals(Range range)
        {
            if (From != range.From)
            {
                return [new Range(From, Math.Max(To, range.To)), new Range(range.From, Math.Max(To, range.To))];
            }

            return [new Range(From, Math.Max(To, range.To))];
        }

        public Range[]? GetIntervalsDifference(Range range)
        {
            if (From < range.From && To > range.To)
            {
                return [new Range(From, range.From), new Range(range.To, To)];
            }

            if (From < range.From && To == range.To)
            {
                return [new Range(From, range.From)];
            }

            if (From == range.From && To > range.To)
            {
                return [new Range(range.To, To)];
            }

            return null;
        }
    }
}
