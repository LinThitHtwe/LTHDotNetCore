using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTHDOtNetCore.PizzaAPI.Models
{
    [Table("PizzaOrderDetail")]
    public class PizzaOrderDetailModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("orderInvoiceNo")]
        public string OrderInvoiceNo { get; set; }
        [Column("pizzaExtraId")]
        public int PizzaExtraId { get; set; }
    }
}
