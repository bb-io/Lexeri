using RestSharp;
using Apps.Lexeri.Api;
using Apps.Lexeri.Invocables;
using Apps.Lexeri.Models.Dto;
using Apps.Lexeri.Models.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Lexeri.Actions;

[ActionList]
public class TermCheckActions(InvocationContext invocationContext): AppInvocable(invocationContext) {    
    [Action("Check text", Description = "Check text for matching terms")]
    public async Task<TermCheckResult> CheckText([ActionParameter] LiveTermCheckRequest input)
    {
        var request = new LexeriRequest(
            "/term_checks/live",
            Method.Post,
            Creds.ToArray()
        );

        request.AddJsonBody(new {
            text = input.Text,
            locale_code = "de"
        });
        
        var response = await Client.ExecuteWithJson<List<TermMatch>>(request);

        return new TermCheckResult
        {
            HasForbiddenTerms = response.Any(static x => x.Matching_term.State == "not_recommended"),
            TermMatches = response.Select(static x => new TermMatch
            {
                Matching_term = x.Matching_term,
                Preferred_term = x.Preferred_term is Array ? null : x.Preferred_term
            }).ToList()
        };
    }
}