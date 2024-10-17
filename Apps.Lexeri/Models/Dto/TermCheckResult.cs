using Blackbird.Applications.Sdk.Common;
namespace Apps.Lexeri.Models.Dto;

public class TermCheckResult
{
    [Display("Includes forbidden terms", Description = "Wether the text includes forbidden terms")]
    public bool HasForbiddenTerms { get; set; }

    [Display("Term matches", Description = "List of term matches found in the text")]
    public List<TermMatch>? TermMatches { get; set; }
}