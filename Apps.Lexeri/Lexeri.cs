using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Lexeri;

public class LexeriApplication :  IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => new[] { ApplicationCategory.QualityManagement, ApplicationCategory.CatAndTms };
        set { }
    }
    
    public string Name
    {
        get => "Lexeri";
        set { }
    }
    
    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}