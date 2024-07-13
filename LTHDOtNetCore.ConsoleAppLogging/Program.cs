using Serilog;
using Serilog.Sinks.MSSqlServer;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/LTHDOtNetCore.ConsoleAppLogging.txt", rollingInterval: RollingInterval.Hour)
            .WriteTo
            .MSSqlServer(
            connectionString: "Server=localhost\\SQLEXPRES;Database=DotnetTrainingBatch4;User ID=sa;Password=root;TrustServerCertificate=True;",
            sinkOptions: new MSSqlServerSinkOptions
            {
            TableName = "LogEvents",
            AutoCreateSqlTable = true
            })
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