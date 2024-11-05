namespace OrderManagementSystem
{
    public class Page
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalSize { get; set; }

        public int TotalPageCount => PageSize == 0
            ? 0
            :
            (int)((decimal)TotalSize / PageSize) == (decimal)TotalSize / PageSize
                ?
                (int)((decimal)TotalSize / PageSize)
                : (int)((decimal)TotalSize / PageSize) + 1;
        public Page()
        {

        }
        public Page(int pageNumber, int pageSize, int totalSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalSize = totalSize;
        }
    }
}
