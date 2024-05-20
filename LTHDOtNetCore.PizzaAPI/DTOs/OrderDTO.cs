namespace LTHDOtNetCore.PizzaAPI.DTOs
{
    public class OrderRequestDTO
    {
        public int PizzaId { get; set; }
        public int[] Extras { get; set; }
    }

    public class OrderResponseDTO
    {
        public string InvoiceNo { get; set; }
        public string Message { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
