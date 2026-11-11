using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_baranova.Data;

var builder = WebApplication.CreateBuilder(args);

// ---- Строка подключения ----
// Локально: ConnectionStrings:DefaultConnection (appsettings.Development.json)
// На Render: переменная окружения DATABASE_URL (пароль в коде и GitHub не хранится)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var databaseUrl = builder.Configuration["DATABASE_URL"];

if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);
    var dbPort = uri.Port > 0 ? uri.Port : 5432;   // во внутреннем URL Render порта может не быть

    // Internal Database URL (хост без точек, например dpg-xxxx-a) — SSL не нужен;
    // External Database URL (хост вида dpg-xxxx.oregon-postgres.render.com) — SSL обязателен.
    var ssl = uri.Host.Contains('.')
        ? "SSL Mode=Require;Trust Server Certificate=true"
        : "SSL Mode=Disable";

    connectionString =
        $"Host={uri.Host};" +
        $"Port={dbPort};" +
        $"Database={uri.AbsolutePath.TrimStart('/')};" +
        $"Username={Uri.UnescapeDataString(userInfo[0])};" +
        $"Password={Uri.UnescapeDataString(userInfo[1])};" +
        ssl;
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Не найдена строка подключения: задайте ConnectionStrings:DefaultConnection или переменную DATABASE_URL.");
}

// ---- EF Core + PostgreSQL ----
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// SuppressImplicit...: поле Description (string, не nullable) может быть пустым в форме
builder.Services.AddRazorPages()
    .AddMvcOptions(o => o.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();

// ---- Автоматическое применение миграций при запуске (учебный вариант) ----
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Красивая страница для кодов 404 и др.: /Status/404
app.UseStatusCodePagesWithReExecute("/Status/{0}");

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

// Render передаёт порт через переменную окружения PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");
