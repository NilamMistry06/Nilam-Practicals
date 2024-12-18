using DotNetCoreWebApp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NewDB")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Minimal API start
app.MapGet("/",()=>"Hello World");

app.MapGet("/greet/{name}", (string name) => $"Hello, {name}!");

var people = new List<Person>();

// POST method to create a new person
app.MapPost("/create", (Person person) => {
    people.Add(person);
    return Results.Created($"/person/{person.Id}", person);
});

// GET method to retrieve all people
app.MapGet("/people", () =>
{
    return Results.Ok(people);
});

// GET method to retrieve a specific person by ID
app.MapGet("/person/{id}",(int id) => {
    var person = people.Find(x => x.Id == id);
    if(person != null)
    {
        return Results.Ok(person);
    }
    else
    {
        return Results.NotFound();
    }
});

//Minimal API end

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public record Person(int Id, string Name);