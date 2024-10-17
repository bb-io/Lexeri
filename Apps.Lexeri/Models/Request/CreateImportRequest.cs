using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Lexeri.Models.Request;

public class CreateImportRequest
{
    [Display("Description", Description = "Description for the term ectraction")]
    public string? Description { get; set; }

    [Display("Document", Description = "Documents to be extracted")]
    public required FileReference Document { get; set; }
}