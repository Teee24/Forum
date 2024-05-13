using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Serilog.Formatting.Compact;
using System.Text;



try
{
    var builder = WebApplication.CreateBuilder(args); //調整builder順序，讓Logger可以方便取用

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug() //設置了最低的日誌記錄級別(級別以上的皆會被記錄)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information) //只記錄 Information 級別以上的 Microsoft 相關日誌
        .Enrich.FromLogContext() //從當前的執行緒上下文中提取相關的上下文信息，並將這些信息自動添加到每條日誌訊息中
        .Enrich.WithEnvironmentName() //Environment
        .ReadFrom.Configuration(builder.Configuration) //通常用於從 appsettings.json 文件中讀取 Serilog 的配置
        .WriteTo.Console() //設置將Log輸出到終端機的畫面上
        .WriteTo.File(new CompactJsonFormatter(), "./logs/log-.json", rollingInterval: RollingInterval.Day) // 將Log輸出為檔案,命名以當天日期為區分  
        .CreateLogger();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog(); //透過 IHostBuilder 加入 UseSerilog()

    var app = builder.Build();

    // Configure the HTTP request pipeline.(middleware)處理所有的HTTP Request和Response
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(options =>
    {
        // 如果要自訂訊息的範本格式，可以修改這裡，但修改後並不會影響結構化記錄的屬性
        options.MessageTemplate = "Handled {RequestPath}";

        // 預設輸出的紀錄等級為 Information，你可以在此修改記錄等級
        // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        // 你可以從 httpContext 取得 HttpContext 下所有可以取得的資訊！
        options.EnrichDiagnosticContext = async (diagnosticContext, httpContext) =>
        {

            var streamReader = new StreamReader(httpContext.Request.Body);
            string requestData = await streamReader.ReadToEndAsync();

            diagnosticContext.Set("RequestBody", requestData);

            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme); //protocol


            diagnosticContext.Set("Protocol", httpContext.Request.Protocol);
            diagnosticContext.Set("Scheme", httpContext.Request.Scheme);
        };

    }); //紀錄每一個Request的資訊，在這邊加入

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