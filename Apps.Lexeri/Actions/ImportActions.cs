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
public class ImportActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient): AppInvocable(invocationContext) {    

    [Action("Import a document", Description = "Create a new import from a TBX, CSV or XLSX document")]
    public async Task<Import> CheckText([ActionParameter] CreateImportRequest input)
    {
      var fileStream = await fileManagementClient.DownloadAsync(input.Document);

      var uploadRequest = new LexeriUploadRequest();
      uploadRequest.AddFile("file", () => fileStream, input.Document.Name);

      var uploadResponse = await Client.ExecuteWithJson<UploadedDocument>(uploadRequest);

      var request = new LexeriRequest(
          "/imports",
          Method.Post,
          Creds
      );

      request.AddJsonBody(new {
          description = input.Description,
          document_token = uploadResponse.Token
      });
      
      return await Client.ExecuteWithJson<Import>(request);
    }
}