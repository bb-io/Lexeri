using Apps.Lexeri.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.Lexeri.Connections;

/// <summary>
/// Describes BlackBird's app connection settings
/// </summary>
public class ConnectionDefinition : IConnectionDefinition
{
    /// <summary>
    /// Defines app's connection types
    /// </summary>
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {        
        // API token auth example
        new()
        {
            Name = "Developer API token",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            
            // Specifying properties that we will need for authorization of the app
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.ApiToken)
                {
                    // Property user-friendly name that will be displayed on the UI
                    DisplayName = "API token",
                    
                    // Setting this flag to true hides token input, replacing each its character with •
                    Sensitive = true,
                    // Description of the connection property,
                    // perhaps with some guidelines on how to find it in the service
                    Description = "Create a new API token in the settings of your termbase in Lexeri."
                }
            }
        }
    };

    /// <summary>
    /// Processes credentials after the authorization is done 
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        // Processing API key credentials
        var apiToken = values.First(v => v.Key == CredsNames.ApiToken);
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.Header,
            apiToken.Key,
            apiToken.Value
        );
    }
}