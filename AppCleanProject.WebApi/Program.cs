using AppCleanProject.WebApi.Middlewares;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration, typeof(Program));
builder.Services.AddInfraestructureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corspolicy");
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthorization();

app.UseWatchDogExceptionLogger();
app.UseWatchDog(config =>
{
    config.WatchPageUsername = "admin";
    config.WatchPagePassword = "admin123";
});

app.MapControllers();

app.Run();
