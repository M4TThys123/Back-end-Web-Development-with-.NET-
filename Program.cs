using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var todos = new List<Todo> ();
app.MapGet("/todos", () => Results.Ok(todos));
app.MapGet("/todos/{id}", (int id) => 
{
    var targetTodo = todos.SingleOrDefault(t => id == t.Id);    
    return targetTodo is null
        ? Results.NotFound()
        : Results.Ok(targetTodo);       
});
app.MapPost("/todos", (Todo task) =>
{
    todos.Add(task);
    return Results.Created($"/todos/{task.Id}", task);
});
app.MapDelete("/todos/{id}", (int id) =>
{
   todos.RemoveAll(t => id == t.Id);
    return Results.NoContent();
});                                                                                                      

app.Run();
public record Todo(int Id, string Name, DateTime DueDate, bool isCompleted);