var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();

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


public interface IStudentRepository
{
    void Add(Student student);

    public Student? Get(Guid id);
}


public class StudentRepository : IStudentRepository
{
    private List<Student> _students;

    public StudentRepository()
    {
        _students = new List<Student>();

        Guid guid = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        _students.Add(new Student { Id=guid, Name="Linux", Major="Programming"});
    }

    public void Add(Student student)
    {
        _students.Add(student);
    }

    public Student? Get(Guid id)
    {
        return _students.FirstOrDefault(s => s.Id == id);

    }
}


public class Student
{
    public Student()
    {
        
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Major { get; set; }
}


