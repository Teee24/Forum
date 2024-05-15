
using JWTAuth_Practice.JwtToken;
using JWTAuth_Practice.TestMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<JWTHelper>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       // �����ҥ��ѮɡA�^�����Y�|�]�t WWW-Authenticate ���Y�A�o�̷|��ܥ��Ѫ��Բӿ��~��]
       options.IncludeErrorDetails = true; // �w�]�Ȭ� true�A���ɷ|�S�O����

       options.TokenValidationParameters = new TokenValidationParameters
       {
           // �z�L�o���ŧi�A�N�i�H�q "sub" ���Ȩó]�w�� User.Identity.Name
           NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
           // �z�L�o���ŧi�A�N�i�H�q "roles" ���ȡA�åi�� [Authorize] �P�_����
           RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

           // �@��ڭ̳��|���� Issuer
           ValidateIssuer = true,
           ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),

           // �q�`���ӻݭn���� Audience
           ValidateAudience = false,
           //ValidAudience = "JwtAuthDemo", // �����ҴN���ݭn��g

           // �@��ڭ̳��|���� Token �����Ĵ���
           ValidateLifetime = true,

           //�@�}�l�]5�����٬O�|�L�A�o�ӭn�]=0
           ClockSkew = TimeSpan.Zero,

           // �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
           ValidateIssuerSigningKey = false,

           // "1234567890123456" ���ӱq IConfiguration ���o
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey")))
       };
   });
//Auth
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMyMiddlewareBefore();
app.UseAuthentication();//������
app.UseMyMiddlewareAfter();

app.UseAuthorization();//�A���v

//app.MapControllers().RequireAuthorization(); ������all controller�A�M��A���n�ocontroller/action�[�W [AllowAnonymous]Attribute
app.MapControllers().RequireAuthorization();

app.Run();


