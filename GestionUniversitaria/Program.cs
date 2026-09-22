using GestionUniversitaria.Data;
using GestionUniversitaria.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// LiteDB
builder.Services.AddSingleton<LiteDbContext>(sp =>
{
    var db = new LiteDbContext("universidad.db");
    db.Seed();

    return db;
});

// Services
builder.Services.AddScoped<CarreraService>();
builder.Services.AddScoped<EstudianteService>();
builder.Services.AddScoped<MateriaService>();
builder.Services.AddScoped<ProfesorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
