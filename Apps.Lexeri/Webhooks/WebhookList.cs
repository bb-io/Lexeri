using System.Net;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using Apps.Lexeri.Models.Response;
using Apps.Lexeri.Webhooks.Handlers;

namespace Apps.Lexeri.Webhooks;

[WebhookList]
public class WebhookList
{
    #region Webhooks

    [Webhook("On term created", typeof(TermCreatedWebhookHandler), Description = "Triggered when a term was created")]
    public Task<WebhookResponse<WebhookPayload>> OnTermCreated(WebhookRequest webhookRequest)
        => HandleCallback<WebhookPayload>(webhookRequest);

    [Webhook("On Term request created", typeof(TermRequestCreatedWebhookHandler), Description = "Triggered when a term request was created")]
    public Task<WebhookResponse<WebhookPayload>> OnTermRequestCreated(WebhookRequest webhookRequest)
        => HandleCallback<WebhookPayload>(webhookRequest);

    [Webhook("On term request finished", typeof(TermRequestFinishedWebhookHandler), Description = "Triggered when processing of a term request was finished by the terminology managers and its draft terms were published")]
    public Task<WebhookResponse<WebhookPayload>> OnTermRequestFinished(WebhookRequest webhookRequest)
        => HandleCallback<WebhookPayload>(webhookRequest);


    [Webhook("On import finished", typeof(ImportFinishedWebhookHandler), Description = "Triggered when processing of an import was finished by the terminology managers and its draft terms were published")]
    public Task<WebhookResponse<WebhookPayload>> OnImportFinished(WebhookRequest webhookRequest)
        => HandleCallback<WebhookPayload>(webhookRequest);

    [Webhook("On task finished", typeof(TaskFinishedWebhookHandler), Description = "Triggered when processing of a task was finished by the terminology managers and its draft terms were published")]
    public Task<WebhookResponse<WebhookPayload>> OnTaskFinished(WebhookRequest webhookRequest)
        => HandleCallback<WebhookPayload>(webhookRequest);

    [Webhook("On term extraction finished", typeof(TermExtractionFinishedWebhookHandler), Description = "Triggered when processing of a term extraction was finished by the terminology managers and its draft terms were published")]
    public Task<WebhookResponse<WebhookPayload>> OnTermExtractionFinished(WebhookRequest webhookRequest)
        => HandleCallback<WebhookPayload>(webhookRequest);

    // [Webhook("Check finished", typeof(CheckFinishedWebhookHandler), Description = "Triggered when a term check of a document is finished. Returns statistics about terms that have been found.")]
    // public Task<WebhookResponse<CheckFinishedWebhookPayload>> OnCheckFinished(WebhookRequest webhookRequest)
    //     => HandleCallback<CheckFinishedWebhookPayload>(webhookRequest);

    #endregion

    #region Utils

    private Task<WebhookResponse<T>> HandleCallback<T>(WebhookRequest request) where T : class
    {
        var payload = JsonConvert.DeserializeObject<T>(request.Body.ToString(),
            new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore });
        
        return Task.FromResult(new WebhookResponse<T>
        {
            HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
            Result = payload
        });
    }

    #endregion
}