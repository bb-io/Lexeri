using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Dto;

public class Import
{
    [Display("ID", Description = "ID of the term request")]
    public required string Identifier { get; set; }

    [Display("File", Description = "Filename of the imported file")]
    public required string File { get; set; }

    [Display("Description", Description = "Description of the term request")]
    public string? Description { get; set; }

    [Display("State", Description = "State of the term request")]
    public required string State { get; set; }

    [Display("Import State", Description = "State of the import process")]
    public required string Import_state { get; set; }

    [Display("Number of terms", Description = "Number of terms included in the import")]
    public required int Number_of_terms { get; set; }

    public required string Token { get; set; }
}