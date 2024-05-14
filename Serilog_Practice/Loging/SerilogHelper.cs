using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;


namespace Serilog_Practice.Loging
{
    public static class SerilogHelper
    {
        public static void ConfigureSerilLogger(IConfiguration configuration)
        {
            string MessageTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {RequestScheme}{RequestPath}/{RequestMethod} Status:{StatusCode} SpentTime: {Elapsed}-{EnvironmentName} {NewLine} ";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information() //設置了最低的日誌記錄級別(級別以上的皆會被記錄)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information) //只記錄 Information 級別以上的 Microsoft 相關日誌
                .Enrich.FromLogContext() //從當前的執行緒上下文中提取相關的上下文信息，並將這些信息自動添加到每條日誌訊息中
                .Enrich.WithEnvironmentName() //Environment
                .Enrich.With(new LogEnricher()) //自定義 Enricher                
               // .WriteTo.Console(outputTemplate: MessageTemplate) //設置將Log輸出到終端機的畫面上
                .WriteTo.Console() //設置將Log輸出到終端機的畫面上
              //  .WriteTo.File(new CompactJsonFormatter(), "./logs/log-.json", rollingInterval: RollingInterval.Day) // 將Log輸出為檔案,命名以當天日期為區分 
                .CreateLogger();

        }

        public static async void EnrichDiagnosticContext(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme); //protocol    
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestPath", httpContext.Request.Path);

            var streamReader = new StreamReader(httpContext.Request.Body);
            string requestData =  await streamReader.ReadToEndAsync();

            diagnosticContext.Set("RequestBody", requestData);
        }
    }
}
