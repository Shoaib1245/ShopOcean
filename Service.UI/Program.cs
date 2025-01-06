using Service.UI.ChatHub;
using Service.UI.EmailSender;
using Service.UI.Utilize;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IApiServiceFrontEnd, IApIServiceFrontEnd>();
builder.Services.AddScoped<IApiServiceAdminn, IApiServiceAdmin>();

builder.Services.AddScoped<EmailTemplate>();
builder.Services.AddScoped<EmailCustTemplate>();

builder.Services.AddScoped<HttpContextAccessor>();

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    // Map SignalR hubs
    endpoints.MapHub<chathubb>("/chathub");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
