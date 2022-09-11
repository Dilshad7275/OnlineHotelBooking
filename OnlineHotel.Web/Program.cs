using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineHotel.BLL;
using OnlineHotel.Models;
using OnlineHotel.Utility;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("default") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
//builder.Services.AddScoped<IDbInitializer,DbInitialize>();
builder.Services.AddMemoryCache();

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
    //DataSeeding();
    app.UseRouting();
    app.UseAuthentication(); ;

    app.UseAuthorization();
    app.MapRazorPages();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

    //void DataSeeding()
    //{
    //    using (var scope = app.Services.CreateScope())
    //    {
    //        var dbInititalizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    //        dbInititalizer.Initialize();
    //    }
    //}



