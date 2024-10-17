using Apps.Lexeri.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.Lexeri.Connections;

/// <summary>
/// Validates app credentials that were provided by the user
/// </summary>
public class ConnectionValidator : IConnectionValidator
{
    private static readonly LexeriClient Client = new();
    
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var request = new LexeriRequest(
            "/termbases/info",
            Method.Get,
            authProviders
        );

        try
        {
            await Client.ExecuteWithHandling(request);
            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            var message = $"Connection validation failed: {ex.Message}";
            
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}