namespace WebAPICommonPitfalls.Common.Utilities
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public PaginationFilter() { }
        public PaginationFilter(int pageNumber, int pageSize) => (PageNumber, PageSize) = (pageNumber, pageSize);
    }
}