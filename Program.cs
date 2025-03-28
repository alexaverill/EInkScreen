var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddTransient<IScreenController, InkyController>();
builder.Services.AddTransient<IScreenController, GenericController>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.Urls.Add("http://0.0.0.0:3030");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
