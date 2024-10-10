using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Request;

public class LiveTermCheckRequest
{
    [Display("Text", Description = "Text to be checked")]
    public required string Text { get; set; }

    [Display("Locale code", Description = "Locale code of the text")]
    public required string LocaleCode { get; set; }
}