using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using apprueba.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using apprueba.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<appruebaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("appruebaContext") ?? throw new InvalidOperationException("Connection string 'appruebaContext' not found.")));
var connectionString = builder.Configuration.GetConnectionString("DbContextSampleConnection") ?? throw new InvalidOperationException("Connection string 'DbContextSampleConnection' not found.");

builder.Services.AddDbContext<DbContextSample>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SampleUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DbContextSample>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
