using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Response;

public class Webhook
{
    [Display("ID", Description = "The ID of the webhook")]
    public required string Identifier { get; set;}

    [Display("Actions", Description = "The actions of the webhooks")]
    public required List<string> Actions { get; set; }

    [Display("Url", Description = "The url of the webhook")]
    public required string Url { get; set; }
}