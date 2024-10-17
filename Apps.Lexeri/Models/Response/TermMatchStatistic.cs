using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Response;

public class TermMatchStatistic
{
    [Display("Preferred terms", Description = "Number of preferred terms")]
    public int Preferred { get; set; }

    [Display("Admitted terms", Description = "Number of admitted terms")]
    public int Admitted { get; set; }

    [Display("Forbidden terms", Description = "Number of forbidden terms")]
    public int Not_recommended { get; set; }

    [Display("Outdated terms", Description = "Number of outdated terms")]
    public int Outdated { get; set; }
}