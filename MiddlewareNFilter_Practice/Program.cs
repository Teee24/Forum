using MiddlewareNFilter_Practice.Filter;
using MiddlewareNFilter_Practice.MiddlewareTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //������UFilter
    options.Filters.Add<ActionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//====�]2��====


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

// �j���N HTTP ������V HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//====��Action�~�]====

app.Run();

//====Run���ᤣ�|�]====


