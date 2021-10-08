using Miracle.WebApi.Filters;
using Miracle.WebApi.JsonConverters;
using Miracle.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(c => c.Filters.Add<ActionExecuteFilter>()).AddJsonOptions(c => c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeConverter()));
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "example.api", Version = "v1" }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseGlobalException();
app.UseResponseTime();
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint($"{(string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..")}/swagger/v1/swagger.json", "example.api v1"));

app.Run();
