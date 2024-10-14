using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Dto;

public class Check
{
    [Display("ID", Description = "ID of the term request")]
    public required string Identifier { get; set; }

    [Display("Document", Description = "Document to be checked")]
    public Document? Document { get; set; }

    [Display("Locale", Description = "Locale of the term check")]
    public List<Locale?>? Locales { get; set; }

    [Display("Progress", Description = "Progress of the asynchronous check")]
    public int? Progress { get; set; }
}