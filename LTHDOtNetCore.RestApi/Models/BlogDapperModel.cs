using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.RestApi.Models
{
    public class BlogDapperModel
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string BlogContent { get; set; }
    }
}
