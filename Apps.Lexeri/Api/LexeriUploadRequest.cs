using Apps.Lexeri.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;

namespace Apps.Lexeri.Api;

public class LexeriUploadRequest : RestRequest
{
  public LexeriUploadRequest() : base(Urls.UploadUrl, Method.Post)
  {
    this.AlwaysMultipartFormData = true;
  }
}