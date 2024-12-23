using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using MrKatsuWeb.Application.Interfaces.Manage;
using MrKatsuWeb.Application.Interfaces.Utilities;
using MrKatsuWeb.Application.Services;
using MrKatsuWeb.Application.Services.Manage;
using MrKatsuWeb.Common;
using MrKatsuWeb.Data.EF;

var builder = WebApplication.CreateBuilder(args);

//1. Thêm CORS Middleware
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

});

var connectionString = builder.Configuration.GetConnectionString(SystemConstants.CONNECTION_STRING);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//5. Đăng kí các dịch vụ DI
var cloudinarySettings = builder.Configuration.GetSection(SystemConstants.CLOUDINARY).Get<CloudinarySettings>();
builder.Services.AddSingleton(new Cloudinary(new Account(
    cloudinarySettings.CloudName,
    cloudinarySettings.ApiKey,
    cloudinarySettings.ApiSecret
)));
builder.Services.AddTransient<IProductManageService, ProductManageService>();
builder.Services.AddTransient<IImageService, ImageService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//1.
app.UseHttpsRedirection();
//2.
app.UseCors("AllowAllOrigins");
//3.
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
