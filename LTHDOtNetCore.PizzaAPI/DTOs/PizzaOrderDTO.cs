namespace LTHDOtNetCore.PizzaAPI.DTOs
{
    public class PizzaOrderDTO
    {
        public class PizzaOrderInvoiceDTO
        {
            public int id { get; set; }
            public string invoiceNo { get; set; }
            public decimal totalAmount { get; set; }
            public int pizzaId { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }
        }

        public class PizzaOrderInvoiceDetailModel
        {
            public int id { get; set; }
            public string orderInvoiceNo { get; set; }
            public int pizzaExtraId { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }
        }

        public class PizzaOrderInvoiceResponse
        {
            public PizzaOrderInvoiceDTO Order { get; set; }
            public List<PizzaOrderInvoiceDetailModel> OrderDetail { get; set; }
        }
    }
}
