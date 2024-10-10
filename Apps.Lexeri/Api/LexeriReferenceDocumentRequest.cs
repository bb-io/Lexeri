using RestSharp;

namespace Apps.Lexeri.Api;

public class LexeriReferenceDocumentRequest : RestRequest
{
  public LexeriReferenceDocumentRequest(string uri, string token) : base(uri, Method.Post)
  {
    this.AddHeader("Authorization", $"Bearer {token}");
  }
}