using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.ModelView
{
    public class ProductVIew
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public int Quantity { get; set; }
        public CategoryDtoRequest? category { get; set; }
    }
}
