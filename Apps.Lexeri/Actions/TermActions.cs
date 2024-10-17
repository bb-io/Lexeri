using RestSharp;
using Apps.Lexeri.Api;
using Apps.Lexeri.Invocables;
using Apps.Lexeri.Models.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Actions;

[ActionList]
public class TermActions(InvocationContext invocationContext)
    : AppInvocable(invocationContext)
{    
    [Action("Search terms", Description = "Search terms by term value")]
    public async Task<List<Models.Dto.Term>> GetTerms([ActionParameter] SearchTermsRequest input)
    {
        var request = new LexeriRequest(
            "/terms",
            Method.Get,
            Creds
        );

        request.AddQueryParameter("search", input.Search);

        var response = await Client.ExecuteWithJson<List<Models.Dto.Term>>(request);

        return response;
    }
}