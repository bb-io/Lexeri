using RestSharp;

namespace Apps.Lexeri.Api;

public class LexeriTermSuggestionRequest : RestRequest
{
  public LexeriTermSuggestionRequest(string uri, string token) : base(uri, Method.Post)
  {
    this.AddHeader("Authorization", $"Bearer {token}");
  }
}