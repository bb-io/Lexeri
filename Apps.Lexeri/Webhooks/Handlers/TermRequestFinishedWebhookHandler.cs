using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Webhooks.Handlers;

public class TermRequestFinishedWebhookHandler : BaseWebhookHandler, IWebhookEventHandler
{
    protected override string SubscriptionEvent => "term_request_finished";

    public TermRequestFinishedWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}