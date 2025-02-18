using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Data;
using DoVuiHaiNao.Models;
using Microsoft.AspNetCore.DataProtection;
using DoVuiHaiNao.Models.Service;
using Microsoft.AspNetCore.Identity.UI.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DoVuiHaiNaoContextConnection") ?? throw new InvalidOperationException("Connection string 'DoVuiHaiNaoContextConnection' not found."); ;
var configation = builder.Configuration;
builder.Services.AddDbContext<DoVuiHaiNaoContext>(options => options.UseSqlite(connectionString));
builder.Services.AddIdentity<ApplicationUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DoVuiHaiNaoContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"/Keys")).SetApplicationName("Demo Application").SetDefaultKeyLifetime(TimeSpan.FromDays(14));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.HttpOnly = true;
    options.LoginPath = $"/Identity/Account/Login";

});
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.RequireUniqueEmail = true;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configation["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configation["Authentication:Google:ClientSecret"];
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=admin}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
