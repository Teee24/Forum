using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Serilog.Formatting.Compact;
using System.Text;



try
{
    var builder = WebApplication.CreateBuilder(args); //�վ�builder���ǡA��Logger�i�H��K����

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug() //�]�m�F�̧C����x�O���ŧO(�ŧO�H�W���ҷ|�Q�O��)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information) //�u�O�� Information �ŧO�H�W�� Microsoft ������x
        .Enrich.FromLogContext() //�q��e��������W�U�夤�����������W�U��H���A�ñN�o�ǫH���۰ʲK�[��C����x�T����
        .Enrich.WithEnvironmentName() //Environment
        .ReadFrom.Configuration(builder.Configuration) //�q�`�Ω�q appsettings.json ���Ū�� Serilog ���t�m
        .WriteTo.Console() //�]�m�NLog��X��׺ݾ����e���W
        .WriteTo.File(new CompactJsonFormatter(), "./logs/log-.json", rollingInterval: RollingInterval.Day) // �NLog��X���ɮ�,�R�W�H��Ѥ�����Ϥ�  
        .CreateLogger();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog(); //�z�L IHostBuilder �[�J UseSerilog()

    var app = builder.Build();

    // Configure the HTTP request pipeline.(middleware)�B�z�Ҧ���HTTP Request�MResponse
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(options =>
    {
        // �p�G�n�ۭq�T�����d���榡�A�i�H�ק�o�̡A���ק��ä��|�v�T���c�ưO�����ݩ�
        options.MessageTemplate = "Handled {RequestPath}";

        // �w�]��X���������Ŭ� Information�A�A�i�H�b���ק�O������
        // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        // �A�i�H�q httpContext ���o HttpContext �U�Ҧ��i�H���o����T�I
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

    }); //�����C�@��Request����T�A�b�o��[�J

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception er)
{

    // �����A�����ε{�������Q�������ҥ~ (Unhandled Exception)
    Log.Fatal(er, "Application terminated unexpectedly");
}
finally
{
    // �N�̫�Ѿl�� Log �g�J�� Sinks �h
    Log.CloseAndFlush();
}