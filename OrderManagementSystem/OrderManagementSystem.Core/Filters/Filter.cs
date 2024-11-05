namespace OrderManagementSystem.Core.Filters
{
    public interface IFilter
    {
        public char Type { get; }
    }
    public class Filter : IFilter
    {
        public Filter()
        {

        }
        public Filter( string name, FilterOperator op, string value)
        {
            Operator = op;
            Name = name;
            Value = value;
        }
        public char Type { get; } = 'F';
        public string Name { get; set; }
        public FilterOperator Operator { get; set; }
        public string Value { get; set; }
    }
    public class CompFilter : IFilter
    {
        public CompFilter() { }
        public CompFilter(FilterOP op, params IFilter[] filters)
        {
            Operator = op;
            Filters = filters.ToList();
        }
        public List<IFilter> ToListIFilter() => new List<IFilter>() { this };
        public List<IFilter> Filters { get; set; }
        public FilterOP Operator { get; set; }
        public char Type { get; } = 'C';


    }
    public enum FilterOP
    {
        AND,
        OR
    }
}
