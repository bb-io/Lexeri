using RestSharp;
using Apps.Lexeri.Api;
using Apps.Lexeri.Invocables;
using Apps.Lexeri.Models.Dto;
using Apps.Lexeri.Models.Request;
using Apps.Lexeri.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;

namespace Apps.Lexeri.Actions;

[ActionList]
public class TermCheckActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient): AppInvocable(invocationContext) {    
    [Action("Check text", Description = "Check text for matching terms")]
    public async Task<TermCheckResult> CheckText([ActionParameter] LiveTermCheckRequest input)
    {
        var request = new LexeriRequest(
            "/term_checks/live",
            Method.Post,
            Creds
        );

        request.AddJsonBody(new {
            text = input.Text,
            locale_code = input.LocaleCode
        });
        
        var response = await Client.ExecuteWithJson<List<TermMatch>>(request);

        return new TermCheckResult
        {
            HasForbiddenTerms = response.Any(static x => x.Matching_term.State == "not_recommended"),
            TermMatches = response.Select(static x => new TermMatch
            {
                Matching_term = x.Matching_term,
                Preferred_term = x.Preferred_term
            }).ToList()
        };
    }

    // [Action("Check document", Description = "Starts a check of a document (asynchronously)")]
    // public async Task<Check> CheckDocument([ActionParameter] CreateDocumentCheckRequest input)
    // {
    //     var fileBytes = fileManagementClient.DownloadAsync(input.Document).Result.GetByteData().Result;

    //     var uploadRequest = new LexeriUploadRequest();
    //     var bytes = await fileStream.GetByteData();
    //     uploadRequest.AddFile("file", bytes, input.Document.Name);

    //     var uploadResponse = await Client.ExecuteWithJson<UploadedDocument>(uploadRequest);

    //     var request = new LexeriRequest(
    //         "/checks",
    //         Method.Post,
    //         Cred
    //     );

    //     request.AddJsonBody(new {
    //         document_token = uploadResponse.Token,
    //         locale_code = input.LocaleCode
    //     });
        
    //     return await Client.ExecuteWithJson<Check>(request);
    // }
}