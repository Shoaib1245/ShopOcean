using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Admin.APIs.AdminDBContext;
using Service.Admin.APIs.Features.Admin.Repository;
using Service.Admin.APIs.Features.Admin.Service;
using Service.Admin.APIs.Features.ChatSystem.Service;
using Service.Admin.APIs.Features.SellerProfileStatus.Repository;
using Service.Admin.APIs.Features.SellerProfileStatus.Service;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Database Connection
builder.Services.AddDbContext<AdminDBContextt>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("AdminDB")));
builder.Services.AddScoped<Func<AdminDBContextt>>((provider) => () => provider.GetRequiredService<AdminDBContextt>());
builder.Services.AddScoped<DbFactory<AdminDBContextt>>();

//Repository
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
    .AddScoped<ISellerProfileStatusRepository, SellerProfileStatusRepository>()
    .AddScoped<IAdminRepository, AdminRepository>();


//Service
builder.Services.AddScoped<SellerProfileStatusService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<ChatSystemService>();


//Java Web Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:issuer"],
        ValidAudience = builder.Configuration["Jwt:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
