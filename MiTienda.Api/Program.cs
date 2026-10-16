using Microsoft.EntityFrameworkCore;
using MiTienda.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// En Railway la cadena llega por la variable ConnectionStrings__Default.
// Los reintentos cubren el arranque de la base (puede tardar en estar lista).
builder.Services.AddDbContext<AppDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        sql => sql.EnableRetryOnFailure()));

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("Web", politica =>
    {
        politica.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});

var app = builder.Build();

// Crea la base de datos y aplica las migraciones al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    // Evita que los Id salten de a 1000 cuando SQL Server se reinicia
    db.Database.ExecuteSqlRaw("ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF;");
}

// Swagger activo tambien publicado, para probar la Api desde el navegador
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.UseHttpsRedirection();
app.UseCors("Web");
app.UseAuthorization();
app.MapControllers();

app.Run();
