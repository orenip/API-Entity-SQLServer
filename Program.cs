using IntroduccionAEFCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Se añade .AddJsonOption para evitar las rutas circulares y entrar en BUCLE INFINITO
builder.Services.AddControllers()
    .AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler= ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//2- Encima del Build, Indicamos que queremos usar SQLSERVER-> appsetting.json (Develoment)-->Creamos Entidades
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer("name=DefaultConnection"));

//Añadido la propiedad de AUTOMAPPER para mapear lo que se inluye en el proyecto.
builder.Services.AddAutoMapper(typeof(Program));

//Ejecución!!
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
