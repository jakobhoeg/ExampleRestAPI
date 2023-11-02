using ExampleRestAPI;
using ExampleRestAPI.Model;
using ExampleRestAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();

builder.Services.Configure<MongoDBRestSettings>(builder.Configuration.GetSection(nameof(MongoDBRestSettings)));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapPost("/students", (IStudentRepository sr, Student student) =>
{
    sr.Add(student);

});


app.MapGet("/student/{id}", (Guid id, IStudentRepository sr) =>
{
    return sr.Get(id);
});


app.Run();




