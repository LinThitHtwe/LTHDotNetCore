using LTHDOtNetCore.ConsoleAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LTHDOtNetCore.ConsoleAPP.Enums.Enum;

namespace LTHDOtNetCore.ConsoleAPP.Helper
{
    internal class PrintData
    {
        public static void PrintBlogData(BlogModel blog)
        {
            Console.WriteLine("---------");
            Console.WriteLine("Id : " + blog.Id);
            Console.WriteLine("Id : " + blog.Title);
            Console.WriteLine("Id : " + blog.Author);
            Console.WriteLine("Id : " + blog.BlogContent);
            Console.WriteLine("---------");
        }

        public static void PrintMutatedStatus(int result, string manipulationMethods)
        {
            Console.WriteLine("---------");
            string status = result > 0 ? "Successfully" : "Fail";

            switch (manipulationMethods)
            {
                case nameof(ManipulationMethods.create):
                    Console.WriteLine($"Create {status}");
                    break;
                case nameof(ManipulationMethods.update):
                    Console.WriteLine($"Update {status}");
                    break;
                case nameof(ManipulationMethods.delete):
                    Console.WriteLine($"Delete {status}");
                    break;
                default:
                    Console.WriteLine($"Unknown operation {manipulationMethods}");
                    break;
            }
            Console.WriteLine("---------");
        }

    }
}
