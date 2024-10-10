using Apps.Lexeri.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;

namespace Apps.Lexeri.Api;

public class LexeriRequest : RestRequest
{
  public LexeriRequest(
    string uri,
    Method method,
    IEnumerable<AuthenticationCredentialsProvider> creds
  ) : base(uri, method)
  {
    var token = creds.Get(CredsNames.ApiToken).Value;
    this.AddHeader("Authorization", $"Bearer {token}");
  }
}