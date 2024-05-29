using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.WindowFormApp.Queries
{
    internal class BlogQuery
    {
        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[blog]
                                                   ([title],[author],[blogContent])
                                                   VALUES
                                                   (@title,@author,@blogContent)";
    }
}
