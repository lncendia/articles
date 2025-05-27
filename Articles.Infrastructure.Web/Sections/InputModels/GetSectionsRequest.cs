namespace Articles.Infrastructure.Web.Sections.InputModels;

/// <summary>
/// 
/// </summary>
public class GetSectionsRequest
{
   /// <summary>
   /// 
   /// </summary>
   public int Skip { get; init; } = 0;
   
   /// <summary>
   /// 
   /// </summary>
   public int Take { get; init; } = 10;
}