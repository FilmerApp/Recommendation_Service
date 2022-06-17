using DAL.Interfaces;
using DAL;
using Logic_Layer;
using Logic_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using FilmContext = DAL.FilmContext;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
        });
});

// Add services to the container.
builder.Services.AddScoped<IRecommendation, Algorithm>();
builder.Services.AddScoped<IFilmData, FilmData>();
builder.Services.AddScoped<IWatchlist, WatchlistData>();

builder.Services.AddDbContext<FilmContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("FilmContext"));
});

builder.Services.AddControllers();
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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
