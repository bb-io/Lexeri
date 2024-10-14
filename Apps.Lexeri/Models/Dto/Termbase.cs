using Blackbird.Applications.Sdk.Common;
namespace Apps.Lexeri.Models.Dto;

public class Termbase
{
    [Display("ID", Description = "ID of the termbase")]
    public required string Identifier { get; set; }

    [Display("Name", Description = "Name of the termbase")]
    public required string Name { get; set; }

    [Display("Description", Description = "Description of the termbase")]
    public string? Description { get; set; }

    [Display("Number of terms", Description = "Number of terms in the termbase")]
    public int Number_of_terms { get; set; }

    [Display("Locales", Description = "Locales of the termbase")]
    public List<Locale?>? Locales { get; set; }
}