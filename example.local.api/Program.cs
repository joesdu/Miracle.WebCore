using Miracle.WebApi.Filters;
using Miracle.WebApi.JsonConverters;
using Miracle.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "example.api", Version = "v1" }));

builder.Services.AddControllers(c => c.Filters.Add<ActionExecuteFilter>()).AddJsonOptions(c =>
  {
      c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.TimeOnlyJsonConverter());
      c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateOnlyJsonConverter());
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) _ = app.UseDeveloperExceptionPage();

app.UseGlobalException();

app.UseResponseTime();
app.UseSwagger().UseSwaggerUI();

app.MapControllers();

app.Run();