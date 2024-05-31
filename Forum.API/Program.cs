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
        .MinimumLevel.Debug() //�]�m�F�̧C����x�O���ŧO(�ŧO�H�W���ҷ|�Q�O��)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information) //�u�O�� Information �ŧO�H�W�� Microsoft ������x
        .Enrich.FromLogContext() //�q��e��������W�U�夤�����������W�U��H���A�ñN�o�ǫH���۰ʲK�[��C����x�T����
        .Enrich.WithEnvironmentName() //Environment
        .WriteTo.Console() //�]�m�NLog��X��׺ݾ����e���W
        .WriteTo.File(formatter: new CompactJsonFormatter(), "./logs_1/log-.json", rollingInterval: RollingInterval.Day) // �NLog��X���ɮ�,�R�W�H��Ѥ�����Ϥ�  
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
    //������Profile
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //�ҥ�CORS
    builder.Services.AddCors(options =>
    {

        options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        //options.AddPolicy("AllowGet", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("*"));
    });

    builder.Host.UseSerilog(); //�z�L IHostBuilder �[�J UseSerilog()

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
        // �A�i�H�q httpContext ���o HttpContext �U�Ҧ��i�H���o����T�I
        options.EnrichDiagnosticContext = async (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme); //protocol
            diagnosticContext.Set("RequestPath", httpContext.Request.Path);

            var streamReader = new StreamReader(httpContext.Request.Body);
            string requestData = await streamReader.ReadToEndAsync();

            diagnosticContext.Set("RequestBody", requestData);
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