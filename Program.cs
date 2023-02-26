using Microsoft.EntityFrameworkCore;
using warehouse_app.utils;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("https://warehouse-frontend-lukyrasocha.vercel.app",
                                             "http://warehouse-frontend-lukyrasocha.vercel.app",
                                             "https://warehouse-frontend-ouh3cj4nwa-ew.a.run.app",
                                             "http://localhost:3000",
                                             "http://localhost")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(GoogleSheetsHelper));

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = $"http://0.0.0.0:{port}";

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

app.Run(url);
