using Forum.API.Infrastructures.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Reflection;

try
{
    var builder = WebApplication.CreateBuilder(args);


    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug() //設置了最低的日誌記錄級別(級別以上的皆會被記錄)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information) //只記錄 Information 級別以上的 Microsoft 相關日誌
        .Enrich.FromLogContext() //從當前的執行緒上下文中提取相關的上下文信息，並將這些信息自動添加到每條日誌訊息中
        .Enrich.WithEnvironmentName() //Environment
        .WriteTo.Console() //設置將Log輸出到終端機的畫面上
        .WriteTo.File(formatter: new CompactJsonFormatter(), "./logs_1/log-.json", rollingInterval: RollingInterval.Day) // 將Log輸出為檔案,命名以當天日期為區分  
        .CreateLogger();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    });

    // DI
    builder.Services.DIConfigurator();

    //AutoMapper
    //找到全部Profile
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //啟用CORS
    builder.Services.AddCors(options =>
    {

        options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        //options.AddPolicy("AllowGet", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("*"));
    });

    builder.Host.UseSerilog(); //透過 IHostBuilder 加入 UseSerilog()

    var app = builder.Build();

    app.UseCors("AllowAll");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(options =>
    {
        // 你可以從 httpContext 取得 HttpContext 下所有可以取得的資訊！
        options.EnrichDiagnosticContext = async (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme); //protocol
            diagnosticContext.Set("RequestPath", httpContext.Request.Path);

            var streamReader = new StreamReader(httpContext.Request.Body);
            string requestData = await streamReader.ReadToEndAsync();

            diagnosticContext.Set("RequestBody", requestData);
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