using Forum.API.Infrastructures.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

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
builder.Services.AddCors(options=> {
    
    //options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    options.AddPolicy("AllowGet", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("*"));
});
var app = builder.Build();

app.UseCors("AllowGet");

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
