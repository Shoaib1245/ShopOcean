using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Category.Repository;
using Service.Seller.APIs.Features.Category.Service;
using Service.Seller.APIs.Features.Price.Repository;
using Service.Seller.APIs.Features.Price.Service;
using Service.Seller.APIs.Features.Product.Repository;
using Service.Seller.APIs.Features.Product.Service;
using Service.Seller.APIs.Features.Seller.Repository;
using Service.Seller.APIs.Features.Seller.Service;
using Service.Seller.APIs.Features.SubCategory.Repository;
using Service.Seller.APIs.Features.SubCategory.Service;
using Service.Seller.APIs.Utility;
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
builder.Services.AddDbContext<DBContextt>(option=>
option.UseSqlServer(builder.Configuration.GetConnectionString("SellerDB")));
builder.Services.AddScoped<Func<DBContextt>>((provider)=> () => provider.GetRequiredService<DBContextt>());
builder.Services.AddScoped<DbFactory<DBContextt>>();

//Repository

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
    .AddScoped<ISellerRepository, SellerRepository>()
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped<ISubCategoryRepository, SubCategoryRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IPriceRepository, PriceRepository>();


//Service

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<PriceService>();
builder.Services.AddScoped<SubCategoryService>();


//Token


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
builder.Services.AddScoped<IFileUploader, FileUploader>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
