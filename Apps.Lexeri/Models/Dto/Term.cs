using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Dto;

public class Term
{
    [Display("ID", Description = "ID of the term")]
    public required string Identifier { get; set; }

    [Display("Value", Description = "Value of the term")]
    public required string Value { get; set; }

    [Display("Usage Note", Description = "Usage note of the term")]
    public string? Usage { get; set; }

    [Display("Usage State", Description = "Usage state of the term")]
    public required string State { get; set; }

    [Display("Grammatical Gender", Description = "Grammatical gender of the term")]
    public string? Grammatical_gender { get; set; }

    [Display("Grammatical Number", Description = "Grammatical number of the term")]
    public string? Grammatical_number { get; set; }

    [Display("Part Of Speech", Description = "Part of speech of the term")]
    public string? Part_of_speech { get; set; }
}