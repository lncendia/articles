namespace Test.Infrastructure.Web.Catalogs.InputModels;

public class SectionTagsRequest
{
    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public List<string>? Tags { get; set; }
}