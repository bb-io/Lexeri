using Blackbird.Applications.Sdk.Common;
namespace Apps.Lexeri.Models.Dto;

public class TermCheckResult
{
    [Display("Includes forbidden terms", Description = "Wether the text includes forbidden terms")]
    public bool HasForbiddenTerms { get; set; }
    public List<TermMatch>? TermMatches { get; set; }
}