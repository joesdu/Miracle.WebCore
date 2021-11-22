using Microsoft.AspNetCore.Mvc;

namespace example.local.api.Controllers;
[Route("[controller]")]
[ApiController]
public class NewTypeController : ControllerBase
{
    [HttpGet]
    public object Get() => new
    {
        Time = new TimeOnly(11, 30, 48),
        Date = new DateOnly(2021, 11, 11)
    };

    [HttpPost]
    public object Post(NewType @new) => new
    {
        Date = DateOnly.Parse(@new.Date!).AddDays(1),
        Time = TimeOnly.Parse(@new.Time!).AddHours(-1),
    };
}

public class NewType
{
    public string? Time { get; set; }
    public string? Date { get; set; }
}