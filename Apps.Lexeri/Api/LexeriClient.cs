using RestSharp;
using Apps.Lexeri.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Apps.Lexeri.Api;

#pragma warning disable CS8603, CS8604 // Possible null reference return.

public class LexeriClient : RestClient
{
    public LexeriClient() : base(new RestClientOptions { BaseUrl = new(Urls.ApiBaseUrl) })
    {
    }
    
    public async Task<T> ExecuteWithJson<T>(string endpoint, Method method, object? bodyObj,
        AuthenticationCredentialsProvider[] creds)
    {
        var response = await ExecuteWithJson(endpoint, method, bodyObj, creds);
        return JsonConvert.DeserializeObject<T>(response.Content);
    }
    
    public async Task<T> ExecuteWithJson<T>(RestRequest request)
    {
        var response = await ExecuteWithHandling(request);
        return JsonConvert.DeserializeObject<T>(response.Content);
    }
    
    private async Task<RestResponse> ExecuteWithJson(string endpoint, Method method, object? bodyObj,
        AuthenticationCredentialsProvider[] creds)
    {
        var request = new LexeriRequest(
            Urls.ApiBaseUrl + endpoint,
            method,
            creds
        );

        if (bodyObj is not null)
            request.WithJsonBody(bodyObj, new()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            });

        return await ExecuteWithHandling(request);
    }

    public async Task<RestResponse>ExecuteWithHandling(RestRequest request)
    {
        var response = await ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
            return response;
        
        throw new(BuildErrorMessage(response));
    }
    
    private string BuildErrorMessage(RestResponse response)
    {
        return $"Status code: {response.StatusCode}, Content: {response.Content}";
    }
}

#pragma warning restore CS8603, CS8604 // Possible null reference return.
