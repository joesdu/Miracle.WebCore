# Miracle.WebCore
一些.Net 6的WebApi常用中间件和一些Filter,以及部分数据类型到Json的转换

# Miracle.WebCore Filter使用?

目前支持异常处理和返回数据格式化

* 使用 Nuget 安装 Miracle.WebCore
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

# Miracle.WebCore JsonConverter使用?

* 该库目前补充的Converter有: DateTimeConverter, DateTimeNullConverter, TimeSpanJsonConverter, TimeOnly, DateOnly
* 其中TimeOnly和DateOnly仅支持.Net API内部使用,传入和传出Json仅支持固定格式字符串
* 如: **`DateOnly👉"2021-11-11"`**, **`TimeOnly👉"23:59:25"`**

* 使用 Nuget 安装 Miracle.WebCore
* 然后在上述 Program.cs 中添加如下内容

* .Net 6 +
```csharp
// Add services to the container.
builder.Services.AddControllers(c =>
{
    c.Filters.Add<ExceptionFilter>(); // 异常处理Filter
    c.Filters.Add<ActionExecuteFilter>(); // 返回数据格式化Filter
}).AddJsonOptions(c =>
{
    c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeConverter());
    c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeNullConverter());
});
```

# Miracle.WebCore 中间件使用?

目前支持全局API执行时间中间件

* 使用 Nuget 安装 # Miracle.WebCore
* 然后在 Program.cs 中添加如下内容

* .Net 6 +
```csharp
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

// 过时
// app.UseResponseTime(); // 全局Action执行时间
app.UseMiracleResponseTime(); // 全局Action执行时间
app.UseAuthorization();

app.MapControllers();
app.UseSwagger().UseSwaggerUI();

app.Run();
```

# .Net 6 中使用3种库的方法集合

* Program.cs 文件

```csharp
using Miracle.WebCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(c => c.AddPolicy("AllowedHosts", c => c.WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts").Split(",")).AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddControllers(c =>
{
    c.Filters.Add<ExceptionFilter>(); // 异常处理Filter
    c.Filters.Add<ActionExecuteFilter>(); // 返回数据格式化Filter
}).AddJsonOptions(c =>
{
    c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeConverter());
    c.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeNullConverter());
});
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "example.api", Version = "v1" }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseMiracleResponseTime();
app.UseCors("AllowedHosts");

app.UseAuthorization();

app.MapControllers();
app.UseSwagger().UseSwaggerUI();

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
miracle-response-time: 5 ms 
```
