namespace LTHDOtNetCore.PizzaAPI.DTOs
{
    public record OrderRequestDTO
    {
        public int PizzaId { get; init; }
        public int[] Extras { get; init; }
    }

    public class OrderResponseDTO
    {
        public string InvoiceNo { get; set; }
        public string Message { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
