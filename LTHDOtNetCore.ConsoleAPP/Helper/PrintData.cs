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
        public static void PrintBlogData(object blog)
        {
            Console.WriteLine("---------");
            foreach (var property in blog.GetType().GetProperties())
            {
                Console.WriteLine($"{property.Name} : {property.GetValue(blog)}");
            }
            Console.WriteLine("---------");
        }


        public static void PrintMutatedStatus(int result, ManipulationMethods manipulationMethods)
        {
            Console.WriteLine("---------");
            string status = result > 0 ? "Successfully" : "Fail";

            switch (manipulationMethods)
            {
                case (ManipulationMethods.create):
                    Console.WriteLine($"Create {status}");
                    break;
                case (ManipulationMethods.update):
                    Console.WriteLine($"Update {status}");
                    break;
                case (ManipulationMethods.delete):
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
