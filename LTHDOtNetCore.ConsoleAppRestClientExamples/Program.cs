using LTHDOtNetCore.ConsoleAppRestClientExamples;

Console.WriteLine("Hello, World!");
Console.ReadLine();

RestClientExample example = new();
await example.RunAsync();

Console.ReadLine();