var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.Run();
