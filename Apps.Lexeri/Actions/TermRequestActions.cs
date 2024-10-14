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
public class TermRequestActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient): AppInvocable(invocationContext) {    

    [Action("Create term request", Description = "Create a term request that can contain term suggestions to be added to the termbase.")]
    public async Task<TermRequest> CheckText([ActionParameter] CreateTermRequestRequest input)
    {
      var request = new LexeriRequest(
          "/term_requests",
          Method.Post,
          Creds.ToArray()
      );

      request.AddJsonBody(new {
          title = input.Title,
          description = input.Description
      });
      
      var termRequest = await Client.ExecuteWithJson<TermRequest>(request);

      if (input.ReferenceDocuments is not null) {
        foreach (var file in input.ReferenceDocuments) {
          if (file is null) {
            continue;
          }

          var fileStream = await fileManagementClient.DownloadAsync(file);

          var uploadRequest = new LexeriUploadRequest();
          uploadRequest.AddFile("file", () => fileStream, file.Name);

          var uploadResponse = await Client.ExecuteWithJson<UploadedDocument>(uploadRequest);

          var addReferenceDocumentRequest = new LexeriReferenceDocumentRequest(
            $"/term_requests/{termRequest.Identifier}/reference_documents",
            termRequest.Token
          );

          addReferenceDocumentRequest.AddJsonBody(new {
            title = file.Name,
            document_token = uploadResponse.Token
          });
          
          await Client.ExecuteWithHandling(addReferenceDocumentRequest);
        }
      }

      if (input.TermSuggestions is not null) {
        foreach (var term in input.TermSuggestions) {

          var addTermSuggestionRequest = new LexeriTermSuggestionRequest(
            $"/terms",
            termRequest.Token
          );

          addTermSuggestionRequest.AddJsonBody(new {
            locale_code = input.LocaleCode,
            value = term,
            state = "preferred"
          });
          
          await Client.ExecuteWithHandling(addTermSuggestionRequest);
        }
      }

      return termRequest;
    }
}