using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LTHDOtNetCore.PizzaAPI.Models
{
    [Table("PizzaOrder")]
    public class PizzaOrderModel
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("invoiceNo")]
        public string InvoiceNo { get; set; }
        [Column("pizzaId")]
        public int PizzaId { get; set; }
        [Column("totalAmount")]
        public decimal TotalAmount { get; set; }
    }
}
