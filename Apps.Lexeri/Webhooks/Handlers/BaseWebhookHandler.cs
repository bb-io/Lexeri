using Apps.Lexeri.Api;
using Apps.Lexeri.Invocables;
using Apps.Lexeri.Models.Response;

using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

public abstract class BaseWebhookHandler : AppInvocable, IWebhookEventHandler
{
  protected abstract string SubscriptionEvent { get; }

  public BaseWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
  {
  }

  public async Task SubscribeAsync(
    IEnumerable<AuthenticationCredentialsProvider> Creds,
    Dictionary<string, string> input
  )
  {
    var actions = new[]  { SubscriptionEvent };
    
    var request = new LexeriRequest(
      "/webhooks",
      Method.Post,
      Creds.ToArray()
    );

    request.AddJsonBody(new {
      url = input["payloadUrl"],
      actions = actions.ToArray(),
    });
    
    await Client.ExecuteWithHandling(request);
  }

  public async Task UnsubscribeAsync(
    IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
    Dictionary<string, string> input
  )
  {
    var request = new LexeriRequest(
      "/webhooks",
      Method.Get,
      Creds.ToArray()
    );

    var webhooks = await Client.ExecuteWithJson<List<Webhook>>(request);

    var identifier = webhooks.Find(webhook => webhook.Actions.Contains(SubscriptionEvent))?.Identifier;

    if (identifier != null)
    {
      var deleteRequest = new LexeriRequest(
        $"/webhooks/{identifier}",
        Method.Delete,
        Creds.ToArray()
      );

      await Client.ExecuteWithHandling(deleteRequest);
    }
  }
}