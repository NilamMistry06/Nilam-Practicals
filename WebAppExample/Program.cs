using Microsoft.EntityFrameworkCore;
using System;
using WebAppExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();//AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.UseSwagger();
//    //app.UseSwaggerUI();
//}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles(); // js use - means if we add additional files
app.UseRouting();
app.UseAuthorization();

//Minimal API start
app.MapGet("/", () => "Hello World");

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
app.MapGet("/person/{id}", (int id) => {
    var person = people.Find(x => x.Id == id);
    if (person != null)
    {
        return Results.Ok(person);
    }
    else
    {
        return Results.NotFound();
    }
});
//Minimal API End

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Location}/{action=FindLocation}/{id?}");

app.Run();

// Sample class to represent a person
public record Person(int Id, string Name);