using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Response;

public class WebhookPayload
{
    [Display("Action", Description = "The action that triggered the webhook")]
    public required string Action { get; set; }

    [Display("User name", Description = "Name of the user that triggered the webhook")]
    public string? User_name { get; set; }

    [Display("ID", Description = "ID of the resource that triggered the webhook")]
    public string? Identifier { get; set; }

    [Display("Title", Description = "Title of the resource that triggered the webhook")]
    public string? Title { get; set; }

    [Display("Description", Description = "Description of the resource that triggered the webhook")]
    public string? Description { get; set; }
}