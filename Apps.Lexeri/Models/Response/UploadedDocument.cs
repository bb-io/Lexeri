using Blackbird.Applications.Sdk.Common;
namespace Apps.Lexeri.Models.Response;

public class UploadedDocument
{
    [Display("Identifier", Description = "Identifier of the uploaded document")]
    public required string Identifier { get; set; }

    [Display("Token", Description = "Token of the upload document")]
    public required string Token { get; set; }

    [Display("Filename", Description = "Filename of the uploaded document")]
    public string? Filename { get; set; }
}