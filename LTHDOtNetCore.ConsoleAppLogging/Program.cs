﻿using Serilog;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/LTHDOtNetCore.ConsoleAppLogging.txt", rollingInterval: RollingInterval.Hour)
            .CreateLogger();

Log.Fatal("Hello, world!");
Log.Error("Hello, Error Phyit Twr p!");

Console.WriteLine("Hello, World!");

Log.Information("Hello, world!");
int a = 10, b = 0;
try
{
    Log.Debug("Dividing {A} by {B}", a, b);
    Console.WriteLine(a / b);
}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
}
finally
{
    await Log.CloseAndFlushAsync();
}