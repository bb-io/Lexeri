using Blackbird.Applications.Sdk.Common;
namespace Apps.Lexeri.Models.Dto;

public class TermMatch
{
    [Display("Matching term", Description = "Term found in the text")]
    public required Term Matching_term { get; set; }

    [Display("Preferred term", Description = "Preferred term for the matching term")]
    public Term? Preferred_term { get; set; }
}