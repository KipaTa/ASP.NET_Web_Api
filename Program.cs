using CharacterCharactersAPI.Services;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovieCharactersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IFranchiseService, FranchiceService>();
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

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
