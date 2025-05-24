using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var useMock = bool.Parse(builder.Configuration["UseMock"] ?? "false");
if (useMock)
{
    Console.WriteLine("Using Mock Screen");
    builder.Services.AddTransient<IScreenController, GenericController>();
}
else
{
    Console.WriteLine("Using screen");
    builder.Services.AddTransient<IScreenController, InkyController>();
}
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:5173");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.Urls.Add("http://0.0.0.0:3030");
app.UseCors();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images")),
    RequestPath = "/images"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
