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

        public static string BlogList { get; } = @"SELECT 
                                                [id],
                                                [title],
                                                [author],
                                                [blogContent]
                                                FROM [dbo].[blog]";

        public static string DeleteBlog { get; } = @"Delete From 
                                                    [dbo].[blog] 
                                                    WHERE id = @id";
    }
}
