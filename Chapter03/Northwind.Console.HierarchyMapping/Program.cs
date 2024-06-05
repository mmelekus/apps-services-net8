using Microsoft.Data.SqlClient; // To use SqlConnectionStringBuilder.
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore; // GenerateCreateScript()
using Northwind.Console.HierarchyMapping.Models; // HierarchyDb, Person, Student, Employee

DbContextOptionsBuilder<HierarchyDb> options = new();
SqlConnectionStringBuilder builder = new();
builder.DataSource = "."; // "ServerName\InstanceName" e.g. @".\sqlexpress"
builder.InitialCatalog = "HierarchyMapping";
builder.TrustServerCertificate = true;
builder.MultipleActiveResultSets = true;
// Because we want to fail faster. Default is 15 seconds.
builder.ConnectTimeout = 3;
// If using Windows Integrated authentication.
builder.IntegratedSecurity = true;
// If using SQL Server authentication.
// builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
// builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");
options.UseSqlServer(builder.ConnectionString);

using (HierarchyDb db = new(options.Options))
{
    bool deleted = await db.Database.EnsureDeletedAsync();
    WriteLine($"Database deleted: {deleted}");

    bool created = await db.Database.EnsureCreatedAsync();
    WriteLine($"Database created: {created}");

    WriteLine("SQL script used to create the database:");
    WriteLine(db.Database.GenerateCreateScript());

    if ((db.Employees is not null) && (db.Students is not null))
    {
        db.Students.Add(new Student { Name = "Connor Roy", Subject = "Politics" });
        db.Employees.Add(new Employee { Name = "Kerry Castellabate", HireDate = DateTime.UtcNow });

        int result = db.SaveChanges();
        WriteLine($"{result} people added.");
    }

    if (db.Students is null || !db.Students.Any())
    {
        WriteLine("There are no students.");
    }
    else
    {
        foreach (Student student in db.Students)
        {
            WriteLine($"{student.Name} studies {student.Subject}");
        }
    }

    if (db.Employees is null || !db.Employees.Any())
    {
        WriteLine("There are no employees.");
    }
    else
    {
        foreach (Employee employee in db.Employees)
        {
            WriteLine($"{employee.Name} was hired on {employee.HireDate}");
        }
    }

    if (db.People is null || !db.People.Any())
    {
        WriteLine("There are no people.");
    }
    else
    {
        foreach (Person person in db.People)
        {
            WriteLine($"{person.Name} has ID of {person.Id}");
        }
    }
}