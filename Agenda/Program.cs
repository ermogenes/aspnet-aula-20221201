using Agenda.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Injeção de dependência
builder.Services.AddDbContext<TarefasContext>(opt => {
    string connectionString = builder.Configuration.GetConnectionString("tarefasConnection")!;
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    opt.UseMySql(connectionString, serverVersion);
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// app.MapGet("/helloworld", () => "Hello World!");

app.MapGet("/api/tarefas", ([FromServices] TarefasContext _db) => {
    var tarefas = _db.Tarefa.ToList();
    return Results.Ok(tarefas);
});

app.MapGet("/api/tarefas/{id}", (
    [FromServices] TarefasContext _db,
    [FromRoute] int id
) => {
    var tarefa = _db.Tarefa.Find(id);

    if (tarefa == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(tarefa);
});

app.Run();
