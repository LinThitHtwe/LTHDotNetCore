// See https://aka.ms/new-console-template for more information
using LTHDOtNetCore.ConsoleAppRefitExamples;

Console.WriteLine("Hello, World!");
Console.ReadLine();

RefitExample refitExample = new();
await refitExample.Run();


Console.ReadLine();