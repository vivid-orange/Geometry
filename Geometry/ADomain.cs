namespace VividOrange.Geometry
{
    public abstract class ADomain<T> : IExtrema<T>
    {
        public T Max { get; }
        public T Min { get; }

        public ADomain(T max, T min)
        {
            Max = max;
            Min = min;
        }
        public override string ToString()
        {
            return $"Domain (Max:{Max} - Min:{Min}";
        }
    }
}
