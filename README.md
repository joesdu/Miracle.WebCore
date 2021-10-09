# Miracle.WebApi
一些.Net 6的WebApi常用中间件和一些Filter,以及部分数据类型到Json的转换

# Miracle.WebApi.Filters 使用?

目前支持异常处理和返回数据格式化

* 使用 Nuget 安装 Miracle.WebApi.Filters
* 然后在 Program.cs 中添加如下内容

* Net 6 +
```csharp
// Add services to the container.
builder.Services.AddControllers(c =>
{
    c.Filters.Add<ExceptionFilter>(); // 异常处理Filter
    c.Filters.Add<ActionExecuteFilter>(); // 返回数据格式化Filter
});
```

# Miracle.WebApi.JsonConverters 使用?

* 该库目前补充的Converter有如下:
DecimalConverter, DecimalNullConverter, DateTimeConverter, DateTimeNullConverter, TimeSpanJsonConvert, IntNullConverter, BoolConverter, BoolNullConverter

* 使用 Nuget 安装 Miracle.WebApi.JsonConverters
* 然后在上述 Program.cs 中添加如下内容

* .Net 6 +
```csharp
// Add services to the container.
builder.Services.AddControllers(c => c.Filters.Add<ActionExecuteFilter>()).AddJsonOptions(c =>
{
    c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeConverter());
    c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeNullConverter());
});
```

# Miracle.WebApi.Middlewares 使用?

目前支持全局异常和全局API执行时间中间件

* 使用 Nuget 安装 # Miracle.WebApi.Middlewares
* 然后在 Program.cs 中添加如下内容

* .Net 6 +
```csharp
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseGlobalException(); // 全局异常中间件
app.UseResponseTime(); // 全局Action执行时间
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "example.api v1"));

app.Run();
```

# .Net 6 中使用3种库的方法集合

* Program.cs 文件

```csharp
using Miracle.WebApi.Filters;
using Miracle.WebApi.JsonConverters;
using Miracle.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(c => c.Filters.Add<ActionExecuteFilter>()).AddJsonOptions(c =>
{
    c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeConverter());
    c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeNullConverter());
});
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "example.api", Version = "v1" }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseGlobalException();
app.UseResponseTime();
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "example.api v1"));

app.Run();
```
* API 响应结果示例
```json
{
  "statusCode": 200,
  "msg": "success",
  "data": [
    {
      "date": "2021-10-10 17:38:16",
      "temperatureC": 6,
      "temperatureF": 42,
      "summary": "Freezing"
    },
    {
      "date": "2021-10-11 17:38:16",
      "temperatureC": 18,
      "temperatureF": 64,
      "summary": "Warm"
    }
  ]
}
```
* Response headers
```
miracle-response-time: 31 ms 
```
