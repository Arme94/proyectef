using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectoef;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>("Data Source=PREDATOR\\SQLEXPRESS;Initial Catalog=TareasDb;user id=sa;password=sa");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbConexion", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.Run();
