using LTHDOtNetCore.ConsoleAPP.Connections;
using LTHDOtNetCore.ConsoleAPP.Examples;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//AdoDotnetExample adoDotnetExample = new AdoDotnetExample();
//adoDotnetExample.GetAll();
//adoDotnetExample.GetById(1);
//adoDotnetExample.GetById(9);
//adoDotnetExample.Create("AddFromConsoleApp Title", "AddFromConsoleApp Author", "AddFromConsoleApp Content");
//adoDotnetExample.Update(1, "Updated Title","Updated Author","Updated Content");
//adoDotnetExample.Delete(3);

//DapperExample dapperExample = new();
//dapperExample.Run();

//EFCoreExample eFCoreExample = new();
//eFCoreExample.Run();

var connectionString = ConnectionString.sqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotnetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();

var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotnetExample>();
adoDotNetExample.GetAll();

Console.ReadKey();