using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Characters;
using MovieCharactersAPI.Services.Franchises;
using MovieCharactersAPI.Services.Movies;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovieCharactersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MovieCharacter API", 
        Description = "APi to get and manipulate information about characters, movies they appear in, and the franchises these movies belong to.",
        Contact = new OpenApiContact
        {
            Name = "Kirsi Tainio & Heidi Joensuu",
            Url = new Uri("https://github.com/KipaTa/ASP.NET_Web_Api"),
        },
        License = new OpenApiLicense
        {
            Name = "MIT 2022",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });
    options.IncludeXmlComments(xmlPath);
});

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
