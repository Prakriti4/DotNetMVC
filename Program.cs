using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ViewModelTableFormation.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Get the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DbContext with SQL Server
builder.Services.AddDbContext<SchoolIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity with the same DbContext
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SchoolIdentityDbContext>();


//Adding authorization for specific role assigned to the user
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdministratorRole",
		 policy => policy.RequireRole("Administrator"));
	options.AddPolicy("RequireUserRole",
		 policy => policy.RequireRole("User"));
});

var app = builder.Build();



// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Enable authentication
app.UseAuthorization();  // Enable authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
