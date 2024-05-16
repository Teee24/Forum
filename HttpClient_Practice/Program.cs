using HttpClient_Practice.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//基本使用方式
//builder.Services.AddHttpClient();

//具名用戶端
//builder.Services.AddHttpClient("Acticities", httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://favqs.com/api/activities/");
//    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
//    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "e3f21f68737d1cf1fb53f3aa38c8218d");
//});

//具型別用戶端
builder.Services.AddHttpClient<ProductsServer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
