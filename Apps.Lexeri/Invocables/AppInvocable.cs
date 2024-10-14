using Apps.Lexeri.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Invocables;

/// <summary>
/// Class with all the object required for performing web requests to the service.
/// Extends BaseInvocable class that contains context information (Flight ID, Bird ID, User credentials, etc.)
/// </summary>
public class AppInvocable : BaseInvocable
{
    #region Properties

    protected LexeriClient Client { get; }

    protected IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    #endregion

    #region Constructors

    protected AppInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new();
    }

    #endregion
}