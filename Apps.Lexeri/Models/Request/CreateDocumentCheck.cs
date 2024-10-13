using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Lexeri.Models.Request;

public class CreateDocumentCheckRequest
{
    [Display("Document", Description = "Documents to be extracted")]
    public required FileReference Document { get; set; }

    [Display("Locale code", Description = "Locale code for the document check")]
    public required string LocaleCode { get; set; }

}