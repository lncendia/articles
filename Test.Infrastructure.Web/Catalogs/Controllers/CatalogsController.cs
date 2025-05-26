using Microsoft.AspNetCore.Mvc;
using Test.Infrastructure.Web.Catalogs.InputModels;

namespace Test.Infrastructure.Web.Catalogs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogsController : ControllerBase
{
    // Получить список разделов
    [HttpGet("sections")]
    public IActionResult GetSections()
    {
        return Ok();
    }

    // Получить список статей в разделе по тэгам
    [HttpPost("sections/articles")]
    public IActionResult GetArticlesInSection([FromBody] SectionTagsRequest request)
    {
        return Ok();
    }
}