using ExampleRestAPI;
using ExampleRestAPI.Model;
using ExampleRestAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPackageRepository, PackageRepository>();

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


app.MapPost("/package", (IPackageRepository sr, Package package) =>
{
    sr.Add(package);

});

app.MapDelete("/package/{id}", (Guid id, IPackageRepository sr) =>
{
    sr.Delete(id);
});


app.MapGet("/package/{id}", (Guid id, IPackageRepository sr) =>
{
    return sr.Get(id);
});

app.MapGet("/packages", (IPackageRepository sr) =>
{
    return sr.GetAll();
});


app.Run();




