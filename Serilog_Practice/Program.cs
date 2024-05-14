using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Serilog.Formatting.Compact;
using Serilog_Practice.Loging;
using System.Text;



try
{
    var builder = WebApplication.CreateBuilder(args); //調整builder順序，讓Logger可以方便取用
    var config = builder.Configuration;

    SerilogHelper.ConfigureSerilLogger(config);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Serillog 
    builder.Services.AddSerilog();

    builder.Host.UseSerilog(); //透過 IHostBuilder 加入 UseSerilog()

    var app = builder.Build();

    // Configure the HTTP request pipeline.(middleware)處理所有的HTTP Request和Response
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = SerilogHelper.EnrichDiagnosticContext); //紀錄每一個Request的資訊，在這邊加入

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception er)
{

    // 紀錄你的應用程式中未被捕捉的例外 (Unhandled Exception)
    Log.Fatal(er, "Application terminated unexpectedly");
}
finally
{
    // 將最後剩餘的 Log 寫入到 Sinks 去
    Log.CloseAndFlush();
}