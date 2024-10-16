using Apps.Lexeri.DataSourceHandlers;

using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Lexeri.Models.Request;

public class CreateDocumentCheckRequest
{
    [Display("Document", Description = "Documents to be extracted")]
    public required FileReference Document { get; set; }

    [Display("Locale code", Description = "Locale code for the document check")]
    [StaticDataSource(typeof(NLPLocaleCodeDataHandler))]
    public required string LocaleCode { get; set; }

}