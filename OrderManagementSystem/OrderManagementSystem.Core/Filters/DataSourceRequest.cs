namespace OrderManagementSystem.Core.Filters
{
    public class DataSourceRequest
    {
        public DataSourceRequest()
        {

        }
        public DataSourceRequest(int pageSize, int pageNumber, List<IFilter> filters, SortDescriptor sort, string defSort)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            Filters = filters;
            Sort = sort;
            DefSort = defSort;  
        }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<IFilter> Filters { get; set; }
        public SortDescriptor? Sort { get; set; }
        public string? DefSort { get; set; }
    }
}
