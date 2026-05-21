using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.ProductDto
{
    public class UpdateProductDto
    {
        public string? Name { get;  set; } = null;
        public decimal? Price { get;  set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; } = null;

    }
}
