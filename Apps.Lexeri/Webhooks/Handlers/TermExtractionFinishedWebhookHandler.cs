using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Webhooks.Handlers;

public class TermExtractionFinishedWebhookHandler : BaseWebhookHandler, IWebhookEventHandler
{
    protected override string SubscriptionEvent => "term_extraction_finished";

    public TermExtractionFinishedWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}