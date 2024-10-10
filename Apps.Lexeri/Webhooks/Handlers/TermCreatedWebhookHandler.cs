using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Webhooks.Handlers;

public class TermCreatedWebhookHandler : BaseWebhookHandler, IWebhookEventHandler
{
    protected override string SubscriptionEvent => "term_created";

    public TermCreatedWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}