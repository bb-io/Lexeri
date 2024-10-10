using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Lexeri.Models.Request;

public class CreateTermRequestRequest
{
    [Display("Title", Description = "Title for the term request")]
    public required string Title { get; set; }

    [Display("Description", Description = "Description for the term request")]
    public string? Description { get; set; }

    [Display("Reference document", Description = "Reference document for the term request")]
    public List<FileReference?>? ReferenceDocuments { get; set; }

    [Display("Term suggestions", Description = "List of suggested terms for the term request")]
    public List<string>? TermSuggestions { get; set; }

    [Display("Locale code", Description = "Locale code of the suggested terms for the term request")]
    public string? LocaleCode { get; set; }
}