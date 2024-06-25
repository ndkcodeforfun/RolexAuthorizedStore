namespace RolexApplication_Backend.Controllers
{
    public class OrderDetailDtoResponse
    {
        public int ProductId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
    }
}
