using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Dto;

public class ReferenceDocument
{
    [Display("ID", Description = "ID of the reference document")]
    public required string Identifier { get; set; }

    [Display("Title", Description = "Title of the reference document")]
    public required string Title { get; set; }

    [Display("Description", Description = "Description of the reference document")]
    public required string Description { get; set; }

    [Display("Download URL", Description = "URL to download the reference document")]
    public required string Download_url { get; set; }
}