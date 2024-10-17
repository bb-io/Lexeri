using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Webhooks.Handlers;

public class ImportFinishedWebhookHandler : BaseWebhookHandler, IWebhookEventHandler
{
    protected override string SubscriptionEvent => "import_finished";

    public ImportFinishedWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}