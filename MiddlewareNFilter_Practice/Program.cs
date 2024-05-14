using MiddlewareNFilter_Practice.Filter;
using MiddlewareNFilter_Practice.MiddlewareTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //全域註冊Filter
    options.Filters.Add<ActionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//====跑2次====


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddlewareTest();
    app.UseMyMiddlewareOne();
    app.UseMyMiddlewareTwo();
    app.UseMiddlewareThird();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 強迫將 HTTP 全部轉向 HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//====有Action才跑====

app.Run();

//====Run之後不會跑====


