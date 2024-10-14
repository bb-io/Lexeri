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
public class TermExtractionActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient): AppInvocable(invocationContext) {    

    [Action("Extract terms from document", Description = "Create a new term extraction that extracts term candidates from given documents.")]
    public async Task<TermExtraction> CheckText([ActionParameter] CreateTermExtractionRequest input)
    {
      var documentTokens = new List<string>();

      foreach (var file in input.Documents) {
        if (file is null) {
          continue;
        }

        var fileStream = await fileManagementClient.DownloadAsync(file);

        var uploadRequest = new LexeriUploadRequest();
        uploadRequest.AddFile("file", () => fileStream, file.Name);

        var uploadResponse = await Client.ExecuteWithJson<UploadedDocument>(uploadRequest);

        documentTokens.Add(uploadResponse.Token);
      }

      var request = new LexeriRequest(
          "/term_extractions",
          Method.Post,
          Creds
      );

      request.AddJsonBody(new {
          title = input.Title,
          description = input.Description,
          locale_code = input.LocaleCode,
          document_tokens = documentTokens
      });
      
      return await Client.ExecuteWithJson<TermExtraction>(request);
    }
}