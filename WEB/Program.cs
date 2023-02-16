using BLL.Services;
using DAL.Context;
using DAL.Data;
using DOMAIN.Identities;
using Microsoft.AspNetCore.Identity;
using WEB.Hubs;
using WEB.Models;

var builder = WebApplication.CreateBuilder(args);

#region builderConfig
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSignalR();
// di
builder.Services.AddScoped<IEmailWork, EmailWork>();
builder.Services.AddScoped<IFileWork, FileWork>();
// crud
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IDalMessage, DalMessage>();
builder.Services.AddScoped<IServiceMessage, ManagerMessage>();
// identy & auth
builder.Services.AddIdentity<AppUser, AppRole>(actions => {
	// password
	actions.Password.RequiredLength = 8;
	actions.Password.RequireDigit = true;
	actions.Password.RequireUppercase = true;
	actions.Password.RequireLowercase = true;
	actions.Password.RequireNonAlphanumeric = false;
	actions.Password.RequiredUniqueChars = 0;

	// user
	actions.User.RequireUniqueEmail = true;

	// lockout
	actions.Lockout.MaxFailedAccessAttempts= 5;
	actions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
})
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(config => {
	// cookie
	CookieBuilder cookie = new CookieBuilder();
	cookie.Name = "AppAuth";
	cookie.MaxAge = TimeSpan.FromHours(12);
	cookie.HttpOnly = true;
	cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
	cookie.SameSite = SameSiteMode.Lax;

	// config
	config.Cookie = cookie;
	config.AccessDeniedPath = "/Account/Register";
	config.LoginPath = "/Account/Login";
	config.SlidingExpiration = true;
	config.ExpireTimeSpan = TimeSpan.FromHours(12);
});
#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapHub<ChatHub>("/services/chatHub");

app.Run();
