using System.Globalization;
using DayEndStatusAPI.Implementation;
using DayEndStatusAPI.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AutoMapper configuration
builder.Services.AddAutoMapper(typeof(Program).Assembly); // Provide the assembly where your AutoMapper profiles are defined

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<IServer1compass, Server1Compass>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
