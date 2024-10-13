using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Models.Response;

public class CheckFinishedWebhookPayload
{
    [Display("Action", Description = "The action that triggered the webhook")]
    public required string Action { get; set; }

    [Display("Identifier", Description = "Identifier of the check")]
    public string Identifier { get; set; }

    [Display("Locale code", Description = "Locale code of the check")]
    public string Locale_code { get; set; }

    [Display("Filename", Description = "Filename of the checked document")]
    public string Filename { get; set; }

    [Display("Term Match Statistics", Description = "Statistic of the term matches")]
    public TermMatchStatistic? Term_statistics { get; set; }
}