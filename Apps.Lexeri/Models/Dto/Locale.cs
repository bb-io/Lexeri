using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Dto;

public class Locale
{
    [Display("Code", Description = "Code of the locale")]
    public required string Code { get; set; }

    [Display("Name", Description = "Name of the locale")]
    public required string Name { get; set; }

}