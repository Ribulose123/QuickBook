
namespace QuickBook.Domain.Common
{
    public class PaginationParams
    {
        public const int MaxiPageSize = 50;

        public int _pageSize = 10;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxiPageSize ? MaxiPageSize : value;
        }

        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "asc";
    }
}
