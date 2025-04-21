using AnimeDessert.Data;
using AnimeDessert.Interfaces;
using AnimeDessert.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Associate service interfaces with their implementations
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ICharacterVersionService, CharacterVersionService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IMusicService, MusicService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IAnimeQuizService, AnimeQuizService>();
builder.Services.AddScoped<IDessertService, DessertService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IInstructionService, InstructionService>();

builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "AnimeAdmin", "DessertAdmin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var credentials = new[] { new[] { "adminuser@gmail.com", "Adminuser1`", "Admin" }, new[] { "animeadmin@gmail.com", "Animeadmin1`", "AnimeAdmin" }, new[] { "dessertadmin@gmail.com", "Dessertadmin1`", "DessertAdmin" }, new[] { "testuser@gmail.com", "Testuser1`", "User" } };

    foreach (var credential in credentials)
    {
        if (await userManager.FindByEmailAsync(credential[0]) == null)
        {
            var user = new IdentityUser();
            user.UserName = credential[0];
            user.Email = credential[0];

            await userManager.CreateAsync(user, credential[1]);
            await userManager.AddToRoleAsync(user, credential[2]);
        }
    }
}

app.Run();
