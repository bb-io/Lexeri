using Apps.Lexeri.DataSourceHandlers;

using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Lexeri.Models.Request;

public class LiveTermCheckRequest
{
    [Display("Text", Description = "Text to be checked")]
    public required string Text { get; set; }

    [Display("Locale code", Description = "Locale code of the text")]
    [StaticDataSource(typeof(NLPLocaleCodeDataHandler))]
    public required string LocaleCode { get; set; }
}