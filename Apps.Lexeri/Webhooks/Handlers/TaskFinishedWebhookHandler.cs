using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Webhooks.Handlers;

public class TaskFinishedWebhookHandler : BaseWebhookHandler, IWebhookEventHandler
{
    protected override string SubscriptionEvent => "task_finished";

    public TaskFinishedWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}