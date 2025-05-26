using Articles.Infrastructure.Web.Catalogs.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Infrastructure.Web.Catalogs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    // Получить список разделов
    [HttpGet]
    public IActionResult GetSections()
    {
        return Ok();
    }

    // Получить список статей в разделе по тэгам
    [HttpGet("Articles")]
    public IActionResult GetArticlesInSection([FromBody] SectionTagsRequest request)
    {
        return Ok();
    }
}