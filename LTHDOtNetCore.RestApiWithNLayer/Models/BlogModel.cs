namespace LTHDOtNetCore.RestApiWithNLayer.Models
{
    [Table("blog")]
    public class BlogModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("author")]
        public string Author { get; set; }
        [Column("blogContent")]
        public string Content { get; set; }
    }


}
