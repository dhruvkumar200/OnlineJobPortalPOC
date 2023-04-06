using OJP.Data;
using OJP.Repository;
using OJP.Business;
using Microsoft.AspNetCore.Authentication.Cookies;
using OJP.Data.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
.AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
 .AddDataAnnotationsLocalization();
builder.Services.AddScoped<IUserBusiness,UserBusiness>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IPostJobBusiness,PostJobBusiness>();
builder.Services.AddScoped<IPostJobRepository,PostJobRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>x.LoginPath=new PathString("/Login/Index"));
builder.Services.AddDbContext<UserDBContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("JobPortalDatabase")));

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
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
