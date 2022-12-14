using backend.Data;
using backend.Utils.Sports;
using backend.Utils.Weather;
using Microsoft.EntityFrameworkCore;



var MyAllowSpecificOrigins = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                          policy.WithMethods("*");
                          policy.WithHeaders("*");
                          policy.WithExposedHeaders("*");
                          policy.AllowAnyHeader();
                      });
});

builder.Services.AddHostedService<WeatherFetchTask>();
builder.Services.AddHostedService<ScoreFetchTask>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
