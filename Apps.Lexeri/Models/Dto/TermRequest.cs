using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Dto;

public class TermRequest
{
    [Display("ID", Description = "ID of the term request")]
    public required string Identifier { get; set; }

    [Display("Title", Description = "Title of the term request")]
    public required string Title { get; set; }

    [Display("Description", Description = "Description of the term request")]
    public string? Description { get; set; }

    [Display("State", Description = "State of the term request")]
    public required string State { get; set; }

    public required string Token { get; set; }
}