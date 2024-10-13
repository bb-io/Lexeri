using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Webhooks.Handlers;

public class CheckFinishedWebhookHandler : BaseWebhookHandler, IWebhookEventHandler
{
    protected override string SubscriptionEvent => "check_finished";

    public CheckFinishedWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}