using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Dto;

public class Document
{
    [Display("Filename", Description = "Filename of the document")]
    public required string Filename { get; set; }

    [Display("Download URL", Description = "URL to download the document")]
    public required string Download_url { get; set; }

}