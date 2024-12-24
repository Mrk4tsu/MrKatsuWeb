using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MrKatsuWeb.Application.Interfaces.Manage;
using MrKatsuWeb.Application.Interfaces.System;
using MrKatsuWeb.Application.Interfaces.Utilities;
using MrKatsuWeb.Application.Services.Manage;
using MrKatsuWeb.Application.Services.System;
using MrKatsuWeb.Common;
using MrKatsuWeb.Data.EF;
using MrKatsuWeb.Data.Entities;

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
//4.
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
//5. Đăng kí các dịch vụ DI
var cloudinarySettings = builder.Configuration.GetSection(SystemConstants.CLOUDINARY).Get<CloudinarySettings>();
builder.Services.AddSingleton(new Cloudinary(new Account(
    cloudinarySettings.CloudName,
    cloudinarySettings.ApiKey,
    cloudinarySettings.ApiSecret
)));
builder.Services.AddTransient<UserManager<User>, UserManager<User>>();
builder.Services.AddTransient<SignInManager<User>, SignInManager<User>>();
builder.Services.AddTransient<RoleManager<Role>, RoleManager<Role>>();
builder.Services.AddTransient<IProductManageService, ProductManageService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IUserService, UserServices>();

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
