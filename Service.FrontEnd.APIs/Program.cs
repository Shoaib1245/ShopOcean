using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.FrontEnd.APIs.DatabaseContext;
using Service.FrontEnd.APIs.Features.Cart.Repository;
using Service.FrontEnd.APIs.Features.Cart.Service;
using Service.FrontEnd.APIs.Features.Customer.Repository;
using Service.FrontEnd.APIs.Features.Customer.Service;
using Service.FrontEnd.APIs.Features.Review_Comments.Repository;
using Service.FrontEnd.APIs.Features.Review_Comments.Service;
using Service.FrontEnd.APIs.Utility;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FrontEndDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("FrontendDBConnection")));


builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<Func<FrontEndDBContext>>((provider) => () => provider.GetRequiredService<FrontEndDBContext>());
builder.Services.AddScoped<DbFactory<FrontEndDBContext>>();

builder.Services.AddScoped<FileUploader>();

//Repository

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
    .AddScoped<ICartRepository, CartRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IReviewRepository, ReviewRepository>();


//Services
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ReviewService>();


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
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
