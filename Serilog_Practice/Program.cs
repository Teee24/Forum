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
    var builder = WebApplication.CreateBuilder(args); //�վ�builder���ǡA��Logger�i�H��K����
    var config = builder.Configuration;

    SerilogHelper.ConfigureSerilLogger(config);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Serillog 
    builder.Services.AddSerilog();

    builder.Host.UseSerilog(); //�z�L IHostBuilder �[�J UseSerilog()

    var app = builder.Build();

    // Configure the HTTP request pipeline.(middleware)�B�z�Ҧ���HTTP Request�MResponse
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = SerilogHelper.EnrichDiagnosticContext); //�����C�@��Request����T�A�b�o��[�J

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