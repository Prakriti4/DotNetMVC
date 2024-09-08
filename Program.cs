using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ViewModelTableFormation.Data;
using ViewModelTableFormation.Helpers;
using ViewModelTableFormation.UnitOfWork;
using ViewModelTableFormation.Services;
using ViewModelTableFormation.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();


// Get the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DbContext with SQL Server
builder.Services.AddDbContext<SchoolIdentityDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()
    )
);


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

//Adding automapper in the solution 
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddScoped<IUnitOfWork, StudentUOW>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();



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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
