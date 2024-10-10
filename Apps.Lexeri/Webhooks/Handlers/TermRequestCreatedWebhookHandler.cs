using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Webhooks.Handlers;

public class TermRequestCreatedWebhookHandler : BaseWebhookHandler, IWebhookEventHandler
{
    protected override string SubscriptionEvent => "term_request_created";

    public TermRequestCreatedWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}