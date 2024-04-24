using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.ConsoleAPP.Model
{
    [Table("blog")]
    public class BlogModel
    {
        [Key]
        [Column("id")]
        public int Id {  get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("author")]
        public string Author { get; set; }
        [Column("blogContent")]
        public string Content { get; set; }
    }
}
