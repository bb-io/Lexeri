using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Request;

public class SearchTermsRequest
{
    [Display("Search", Description = "Search query")]
    public string? Search { get; set; }
}