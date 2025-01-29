namespace RangeTask
{
    internal class Range(double from, double to)
    {
        public double From { get; set; } = from;
        public double To { get; set; } = to;

        public double GetLength()
        {
            return To - From;
        }

        public bool IsInside(double number)
        {
            return number >= From && number <= To;
        }
    }
}
