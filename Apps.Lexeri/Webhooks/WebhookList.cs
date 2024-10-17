using System.Net;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using Apps.Lexeri.Models.Response;
using Apps.Lexeri.Webhooks.Handlers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Lexeri.Webhooks;

[WebhookList]
public class WebhookList
{
    #region Webhooks

    [Webhook("On term created", typeof(TermCreatedWebhookHandler), Description = "Triggered when a term was created")]
    public Task<WebhookResponse<WebhookPayload>> OnTermCreated(WebhookRequest webhookRequest, [WebhookParameter][Display("Term ID")] string? identifier)
        => HandleCallback(webhookRequest, identifier);

    [Webhook("On term request created", typeof(TermRequestCreatedWebhookHandler), Description = "Triggered when a term request was created")]
    public Task<WebhookResponse<WebhookPayload>> OnTermRequestCreated(WebhookRequest webhookRequest, [WebhookParameter][Display("Term request ID")] string? identifier)
        => HandleCallback(webhookRequest, identifier);

    [Webhook("On term request finished", typeof(TermRequestFinishedWebhookHandler), Description = "Triggered when processing of a term request was finished by the terminology managers and its draft terms were published")]
    public Task<WebhookResponse<WebhookPayload>> OnTermRequestFinished(WebhookRequest webhookRequest, [WebhookParameter][Display("Term request ID")] string? identifier)
        => HandleCallback(webhookRequest, identifier);

    [Webhook("On import finished", typeof(ImportFinishedWebhookHandler), Description = "Triggered when processing of an import was finished by the terminology managers and its draft terms were published")]
    public Task<WebhookResponse<WebhookPayload>> OnImportFinished(WebhookRequest webhookRequest, [WebhookParameter][Display("Import ID")] string? identifier)
        => HandleCallback(webhookRequest, identifier);

    [Webhook("On task finished", typeof(TaskFinishedWebhookHandler), Description = "Triggered when processing of a task was finished by the terminology managers and its draft terms were published")]
    public Task<WebhookResponse<WebhookPayload>> OnTaskFinished(WebhookRequest webhookRequest, [WebhookParameter][Display("Task ID")] string? identifier)
        => HandleCallback(webhookRequest, identifier);

    [Webhook("On term extraction finished", typeof(TermExtractionFinishedWebhookHandler), Description = "Triggered when processing of a term extraction was finished by the terminology managers and its draft terms were published")]
    public Task<WebhookResponse<WebhookPayload>> OnTermExtractionFinished(WebhookRequest webhookRequest, [WebhookParameter][Display("Term extraction ID")] string? identifier)
        => HandleCallback(webhookRequest, identifier);

    // [Webhook("Check finished", typeof(CheckFinishedWebhookHandler), Description = "Triggered when a term check of a document is finished. Returns statistics about terms that have been found.")]
    // public Task<WebhookResponse<CheckFinishedWebhookPayload>> OnCheckFinished(WebhookRequest webhookRequest)
    //     => HandleCallback<CheckFinishedWebhookPayload>(webhookRequest);

    #endregion

    #region Utils

    private async Task<WebhookResponse<WebhookPayload>> HandleCallback(WebhookRequest request, string? identifier)
    {
        var payload = JsonConvert.DeserializeObject<WebhookPayload>(request.Body.ToString(),
            new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore });

        if (identifier != null && payload.Identifier != identifier)
        {
            return new()
            {
                HttpResponseMessage = null,
                Result = null,
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }

        return new WebhookResponse<WebhookPayload>
        {
            HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
            Result = payload
        };
    }

    #endregion
}