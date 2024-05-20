using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTHDOtNetCore.PizzaAPI.Models
{
    [Table("PizzaExtras")]
    public class PizzaExtraModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
    }
}
