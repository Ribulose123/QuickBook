using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto
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
    }
}
