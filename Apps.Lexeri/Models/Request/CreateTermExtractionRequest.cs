using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Lexeri.Models.Request;

public class CreateTermExtractionRequest
{
    [Display("Title", Description = "Title for the term ectraction")]
    public required string Title { get; set; }

    [Display("Description", Description = "Description for the term ectraction")]
    public string? Description { get; set; }

    [Display("Locale code", Description = "Locale code for the term ectraction")]
    public required string LocaleCode { get; set; }

    [Display("Documents", Description = "Documents to be extracted")]
    public required List<FileReference> Documents { get; set; }
}