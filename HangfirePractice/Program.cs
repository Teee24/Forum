using Hangfire;
using Hangfire.SqlServer;
using HangfirePractice;
using HangfirePractice.Controllers;
using HangfirePractice.Service;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-TW");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//啟用CORS
builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    //options.AddPolicy("AllowGet", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("*"));
});

builder.Services.AddHangfire(config =>
{
    config.UseDefaultCulture(new CultureInfo("zh-TW"))
          .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("Forum"), new SqlServerStorageOptions
          {
              CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
              QueuePollInterval = TimeSpan.Zero,
              UseRecommendedIsolationLevel = true,
              UsePageLocksOnDequeue = true,
              DisableGlobalLocks = true
          });
});

builder.Services.AddHangfireServer();


builder.Services.AddScoped<IStockService, StockService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 使用 Hangfire 儀表板
// 改url
app.UseHangfireDashboard("/job", new DashboardOptions
{
    DashboardTitle = "Hangfire 排程練習",
    StatsPollingInterval = 2000,
    DarkModeEnabled = true
});

JobScheduler.ScheduleRecurringJobs();

app.MapControllers();
app.Run();
