using SoSucKhoe.Models.Main;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SosuckhoeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeDBConnectionString"));
    options.EnableSensitiveDataLogging(false);
});

// Cấu hình xác thực
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddControllersWithViews();

// Cấu hình session
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
};


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Tiemchung",
    pattern: "/tiemchung",
    defaults: new { controller = "Home", action = "Tiemchung" }
);
app.MapControllerRoute(
    name: "Login",
    pattern: "/login",
    defaults: new { controller = "Account", action = "Login" }
);
app.MapControllerRoute(
    name: "Register",
    pattern: "/register",
    defaults: new { controller = "Account", action = "Register" }
);
app.MapControllerRoute(
    name: "ForgotPassword",
    pattern: "/forgot",
    defaults: new { controller = "Account", action = "ForgotPassword" }
);
app.MapControllerRoute(
    name: "Thongtinsuckhoe",
    pattern: "/thongtinsuckhoe",
    defaults: new { controller = "Health", action = "Thongtinsuckhoe" }
);
app.MapControllerRoute(
    name: "Suckhoehangngay",
    pattern: "/suckhoehangngay",
    defaults: new { controller = "Health", action = "Suckhoehangngay" }
);
app.MapControllerRoute(
    name: "Chiphi",
    pattern: "/chiphi",
    defaults: new { controller = "Health", action = "Chiphi" }
);
app.MapControllerRoute(
    name: "Theodoisuckhoe",
    pattern: "/theodoisuckhoe",
    defaults: new { controller = "Health", action = "Theodoisuckhoe" }
);
app.MapControllerRoute(
    name: "Lichtiemchung",
    pattern: "/lichtiemchung",
    defaults: new { controller = "Health", action = "LichTiemchung" }
);
app.MapControllerRoute(
    name: "Dangkitiemchung",
    pattern: "/dangkitiemchung",
    defaults: new { controller = "Health", action = "Dangkitiemchung" }
);
app.MapControllerRoute(
    name: "Thongtinsaukham",
    pattern: "/thongtinsaukham",
    defaults: new { controller = "Health", action = "Thongtinsaukham" }
);
app.MapControllerRoute(
    name: "Phieukhamsuckhoe",
    pattern: "/phieukhamsuckhoe",
    defaults: new { controller = "Health", action = "Phieukhamsuckhoe" }
);
app.MapControllerRoute(
    name: "Phieusuckhoedinhki",
    pattern: "/phieusuckhoedinhki",
    defaults: new { controller = "Health", action = "Phieusuckhoedinhki" }
);
app.MapControllerRoute(
    name: "Thongbao",
    pattern: "/thongbao",
    defaults: new { controller = "Account", action = "Thongbao" }
);
app.MapControllerRoute(
    name: "Chitietlichtiem",
    pattern: "/chitietlichtiem",
    defaults: new { controller = "Health", action = "Chitietlichtiem" }
);
app.Run();
