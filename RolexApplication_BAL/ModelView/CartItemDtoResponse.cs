using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.ModelView
{
    public class CartItemDtoResponse
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public ProductVIew ProductVIew { get; set; } = new ProductVIew();
    }
}
