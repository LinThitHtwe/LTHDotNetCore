using LTHDOtNetCore.ConsoleAppHttpClientExamples;

Console.WriteLine("Program Started");

Console.ReadLine();

Console.WriteLine("You Clicked");

HttpClientExample example = new();
await example.RunAsync();

Console.ReadLine();