
using LTHDOtNetCore.ConsoleAPP;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

AdoDotnetExample adoDotnetExample = new AdoDotnetExample();
//adoDotnetExample.GetAll();
adoDotnetExample.GetById(1);
adoDotnetExample.GetById(9);
//adoDotnetExample.Create("AddFromConsoleApp Title", "AddFromConsoleApp Author", "AddFromConsoleApp Content");
//adoDotnetExample.Update(1, "Updated Title","Updated Author","Updated Content");
//adoDotnetExample.Delete(3);
Console.ReadKey();