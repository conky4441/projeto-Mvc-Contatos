using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Api.Data;
using ProjetoMVC.Api.Helper;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Repositories;
using ProjetoMVC.Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString =
    Environment.GetEnvironmentVariable("DB_CONNECTION")
    ?? builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var smtpSettings = new SmtpSettings
{
    SmtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "",
    SmtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "0"),
    FromEmail = Environment.GetEnvironmentVariable("EMAIL_FROM") ?? "",
    FromName = Environment.GetEnvironmentVariable("EMAIL_FROM_NAME") ?? "",
    Username = Environment.GetEnvironmentVariable("EMAIL_USERNAME") ?? "",
    Password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD") ?? ""
};

builder.Services.AddSingleton(smtpSettings);

builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmail, Email>();


builder.Services.AddSession(a =>
{
    a.Cookie.HttpOnly = true;
    a.Cookie.IsEssential = true;
});

var app = builder.Build();
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");


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

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
