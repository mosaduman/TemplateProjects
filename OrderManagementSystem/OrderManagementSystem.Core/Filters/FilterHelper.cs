namespace OrderManagementSystem.Core.Filters
{
    public static class FilterHelper
    {
        private static string IsLessThan(this string name, string value, string t = "") => $" {name.GetTName(t)} < '{value}' ";
        private static string IsLessThanOrEqualTo(this string name, string value, string t = "") => $" {name.GetTName(t)} <= '{value}' ";
        private static string IsEqualTo(this string name, string value, string t = "") => $" {name.GetTName(t)} = '{value}' ";
        private static string IsNotEqualTo(this string name, string value, string t = "") => $" {name.GetTName(t)} != '{value}' ";
        private static string IsGreaterThanOrEqualTo(this string name, string value, string t = "") => $" {name.GetTName(t)} >= '{value}' ";
        private static string IsGreaterThan(this string name, string value, string t = "") => $" {name.GetTName(t)} > '{value}' ";
        private static string StartsWith(this string name, string value, string t = "") => $" {name.GetTName(t)} LIKE '{value}%' ";
        private static string EndsWith(this string name, string value, string t = "") => $" {name.GetTName(t)} LIKE '%{value}' ";
        private static string Contains(this string name, string value, string t = "") => $" {name.GetTName(t)} LIKE '%{value}%' ";
        private static string IsContainedIn(this string name, string value, string t = "") => $" {name.GetTName(t)} LIKE '%{value}%' ";
        private static string DoesNotContain(this string name, string value, string t = "") => $" {name.GetTName(t)} NOT LIKE '%{value}%' ";
        private static string IsNull(this string name, string value, string t = "") => $" {name.GetTName(t)} IS NULL ";
        private static string IsNotNull(this string name, string value, string t = "") => $" {name.GetTName(t)} IS NOT NULL";
        private static string IsEmpty(this string name, string value, string t = "") => $" {name.GetTName(t)} = '' ";
        private static string IsNotEmpty(this string name, string value, string t = "") => $" {name.GetTName(t)} != '' ";
        private static string IsNullOrEmpty(this string name, string value, string t = "") => $" ({name.GetTName(t)} IS NULL OR {name.GetTName(t)} = '') ";
        private static string IsNotNullOrEmpty(this string name, string value, string t = "") => $" ({name.GetTName(t)} IS NOT NULL AND {name.GetTName(t)} != '') ";
        private static string GetTName(this string name, string t) => $"{(string.IsNullOrWhiteSpace(t) ? name : $"{t}.{name}")}";
        private static string ToQueryable(this string query, string t) => $"SELECT * FROM ({query}) {t} ";
        private static string ToSort(this string query, SortDescriptor descriptor, string def) => $"SELECT ROW_NUMBER() OVER (ORDER BY {(descriptor == null ? def : $"{descriptor.Name} {descriptor.Direction}")} ) AS R_NUMBER,RR_N.* FROM ( {query} ) RR_N";
        private static string ToFilter(this Filter filter, string t = "")
        {
            if (filter == null) return "";
            var r = string.Empty;
            switch (filter.Operator)
            {
                case FilterOperator.IsLessThan:
                    r = filter.Name.IsLessThan(filter.Value, t);
                    break;
                case FilterOperator.IsLessThanOrEqualTo:
                    r = filter.Name.IsLessThanOrEqualTo(filter.Value, t);
                    break;
                case FilterOperator.IsEqualTo:
                    r = filter.Name.IsEqualTo(filter.Value, t);
                    break;
                case FilterOperator.IsNotEqualTo:
                    r = filter.Name.IsNotEqualTo(filter.Value, t);
                    break;
                case FilterOperator.IsGreaterThanOrEqualTo:
                    r = filter.Name.IsGreaterThanOrEqualTo(filter.Value, t);
                    break;
                case FilterOperator.IsGreaterThan:
                    r = filter.Name.IsGreaterThan(filter.Value, t);
                    break;
                case FilterOperator.StartsWith:
                    r = StartsWith(filter.Name, filter.Value, t);
                    break;
                case FilterOperator.EndsWith:
                    r = EndsWith(filter.Name, filter.Value, t);
                    break;
                case FilterOperator.Contains:
                    r = Contains(filter.Name, filter.Value, t);
                    break;
                case FilterOperator.IsContainedIn:
                    r = filter.Name.IsContainedIn(filter.Value, t);
                    break;
                case FilterOperator.DoesNotContain:
                    r = filter.Name.DoesNotContain(filter.Value, t);
                    break;
                case FilterOperator.IsNull:
                    r = filter.Name.IsNull(filter.Value, t);
                    break;
                case FilterOperator.IsNotNull:
                    r = filter.Name.IsNotNull(filter.Value, t);
                    break;
                case FilterOperator.IsEmpty:
                    r = filter.Name.IsEmpty(filter.Value, t);
                    break;
                case FilterOperator.IsNotEmpty:
                    r = filter.Name.IsNotEmpty(filter.Value, t);
                    break;
                case FilterOperator.IsNullOrEmpty:
                    r = filter.Name.IsNullOrEmpty(filter.Value, t);
                    break;
                case FilterOperator.IsNotNullOrEmpty:
                    r = filter.Name.IsNotNullOrEmpty(filter.Value, t);
                    break;
                default:
                    r = Contains(filter.Name, filter.Value, t);
                    break;
            }
            return r;
        }
        private static string ToCompFilter(this CompFilter filter, string t = "")
        {
            //if (filter == null) return "";
            //var first = filter.Filters.First();
            //var last = filter.Filters.Last();
            //var r = $" ( {(first.GetType().Name == typeof(Filter).Name ? ((Filter)first).ToFilter(t) : ((CompFilter)first).ToCompFilter(t))} {filter.Operator} {(last.GetType().Name == typeof(Filter).Name ? ((Filter)last).ToFilter(t) : ((CompFilter)last).ToCompFilter(t))}) ";
            //return r;

            if (filter == null) return "";
            var op = filter.Operator;
            var query = "( ";
            for (int i = 0; i < filter.Filters.Count; i++)
            {
                var item = filter.Filters[i];
                query +=
                    item.GetType().Name == typeof(Filter).Name ?
                        $" {(item as Filter).ToFilter(t)} " :
                        $" {(item as CompFilter).ToCompFilter(t)} ";
                if (i != filter.Filters.Count - 1)
                    query += $" {op} ";
            }
            query += " )";
            return query;
        }
        private static string ToBetween(int page, int pageSize, string t = "") => $" {"R_NUMBER".GetTName(t)} BETWEEN {((page - 1) * pageSize) + 1} AND {page * pageSize} ";
        public static string GetFilterString(this string query, DataSourceRequest request, bool isPaging = true)
        {
            var qu = $"{query.ToQueryable("T")} ";
            var f = "";
            if (request.Filters != null && request.Filters.Any())
            {
                f = $" WHERE ";
                for (int i = 0; i < request.Filters.Count; i++)
                {
                    var filter = request.Filters[i];
                    if (i != 0)
                        f += " AND ";
                    if (filter.GetType().Name == typeof(Filter).Name)
                        f += $" {((Filter)filter).ToFilter("T")} ";
                    else
                        f += $" {((CompFilter)filter).ToCompFilter("T")} ";
                }
            }

            qu = $"{qu} {f}".ToSort(request.Sort, request.DefSort);
            qu = $"{qu.ToQueryable("L")} {(isPaging ? $" WHERE {ToBetween(request.PageNumber, request.PageSize, "L")}" : "")}";
            return qu;
        }
        public static string GetFilterCountString(this string query, DataSourceRequest request)
        {
            var qu = $"{query.ToQueryable("T")} ";
            var f = "";
            if (request.Filters != null && request.Filters.Any())
            {
                f = $" WHERE ";
                for (int i = 0; i < request.Filters.Count; i++)
                {
                    var filter = request.Filters[i];
                    if (i != 0)
                        f += " AND ";
                    if (filter.GetType().Name == typeof(Filter).Name)
                        f += $" {((Filter)filter).ToFilter("T")} ";
                    else
                        f += $" {((CompFilter)filter).ToCompFilter("T")} ";
                }
            }
            qu = $"{qu} {f}";
            qu = $"SELECT COUNT(*) AS TotalSize FROM ( {qu} ) CC";
            return qu;
        }

        public static List<IFilter> CreateFilter(FilterOP? op, params IFilter[] filters) => op.HasValue ? new CompFilter(op.Value, filters).ToListIFilter() : new List<IFilter>();
        public static DataSourceRequest CreateDataSource(int pageSize, int pageNumber, SortDescriptor sort, FilterOP? op, params IFilter[] filter) => new DataSourceRequest(pageSize, pageNumber, CreateFilter(op, filter), sort, sort?.Name ?? "Id");
        public static List<Filter> ToFilterDesc(this List<IFilter> request)
        {
            var result = new List<Filter>();
            if (request.Any())
            {
                foreach (var filter in request)
                {
                    var descriptor = filter as Filter;
                    if (descriptor != null)
                    {
                        result.Add(descriptor);
                    }
                    else
                    {
                        var compositeFilterDescriptor = filter as CompFilter;
                        if (compositeFilterDescriptor != null)
                        {
                            result.AddRange(compositeFilterDescriptor.Filters.ToFilterDesc());
                        }
                    }
                }
            }
            return result;
        }
        public static List<Filter> ToFilterDescriptorRemoveItem(this IList<IFilter> filters, string name)
        {
            var result = new List<Filter>();
            if (filters.Any())
            {
                for (int i = 0; i < filters.Count; i++)
                {
                    var descriptor = filters[i] as Filter;
                    if (descriptor != null)
                    {
                        if (descriptor.Name == name)
                        {
                            filters.Remove(filters[i]);

                            result.Add(descriptor);

                        }
                    }
                    else
                    {
                        var compositeFilterDescriptor = filters[i] as CompFilter;
                        if (compositeFilterDescriptor != null)
                        {
                            result.AddRange(compositeFilterDescriptor.Filters.ToFilterDescriptorRemoveItem(name));
                        }
                    }
                }
            }
            return result;
        }
    }
}
