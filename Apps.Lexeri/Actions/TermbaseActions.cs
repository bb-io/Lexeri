using System.Net.Mime;

using RestSharp;
using Apps.Lexeri.Api;
using Apps.Lexeri.Invocables;
using Apps.Lexeri.Models.Response;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Glossaries.Utils.Dtos;
using Blackbird.Applications.Sdk.Glossaries.Utils.Dtos.Enums;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using System.Xml;

namespace Apps.Lexeri.Actions;

[ActionList]
public class TermbaseActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{    
    [Action("Get termbase info", Description = "Get termbase by identifier")]
    public async Task<Models.Dto.Termbase> GetTermbase()
    {
        var request = new LexeriRequest(
            "/termbases/info",
            Method.Get,
            Creds
        );

        var response = await Client.ExecuteWithJson<Models.Dto.Termbase>(request);

        return response;
    }

    [Action("Export termbase", Description = "Export the preferred and admitted terms of the termbase as tbx file")]
    public async Task<GlossaryWrapper> ExportTermbase()
    {
        var request = new LexeriRequest(
            "/exports/download",
            Method.Post,
            Creds
        );

        var response = await Client.ExecuteAsync<string>(request);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to export termbase");

        var glossary = ConvertXmlTermbaseToGlossary(response.Content!);
        var glossaryStream = glossary.ConvertToTbx();

        var glossaryFileReference =
            await fileManagementClient.UploadAsync(glossaryStream, MediaTypeNames.Text.Xml,
                "lexeri_export.tbx");
        
        return new() { Glossary = glossaryFileReference };
    }

    private Glossary ConvertXmlTermbaseToGlossary(string xmlContent) {
        var partOfSpeechAbbreviations = new Dictionary<string, string>
        {
            { "adj", "Adjective" },
            { "adv", "Adverb" },
            { "conj", "Conjunction" },
            { "interj", "Interjection" },
            { "noun", "Noun" },
            { "prep", "Preposition" },
            { "pron", "Pronoun" },
            { "proper_noun", "ProperNoun" },
            { "verb", "Verb" }
        };

        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlContent);

        var conceptEntries = new List<GlossaryConceptEntry>();
        var glossary = new Glossary(conceptEntries)
        {
            Title = xmlDocument.SelectSingleNode("//martif/martifHeader/fileDesc/titleStmt/title")?.InnerText,
            SourceDescription = xmlDocument.SelectSingleNode("//martif/martifHeader/fileDesc/sourceDesc")?.InnerText
        };

        var termEntryNodes = xmlDocument.SelectNodes("//martif/text/body/termEntry")!;

        foreach (XmlElement termEntryNode in termEntryNodes)
        {
            var languageGroupNodes = termEntryNode.SelectNodes("langSet")!;
            var languageSections = new List<GlossaryLanguageSection>();

            foreach (XmlElement languageNode in languageGroupNodes)
            {
                var language = languageNode.Attributes!["xml:lang"]!.Value.ToLower();
                var termSections = new List<GlossaryTermSection>();

                var tigNodes = languageNode.SelectNodes("tig")!;

                foreach (XmlElement tigNode in tigNodes)
                {
                    var termStatus = tigNode
                        .SelectNodes("termNote")?
                        .Cast<XmlElement>()
                        .FirstOrDefault(termNote => termNote.Attributes["type"]?.Value == "administrativeStatus")?
                        .InnerText;
                    
                    if (termStatus == "not_recommended" || termStatus == "deprecatedTerm-admn-sts")
                        continue;

                    var term = tigNode.SelectSingleNode("term")!.InnerText;

                    var termSection = new GlossaryTermSection(term);

                    var usageNote = tigNode
                        .SelectNodes("termNote")?
                        .Cast<XmlElement>()
                        .FirstOrDefault(termNote => termNote.Attributes["type"]?.Value == "usageNote")?
                        .InnerText;

                    if (usageNote != null) {
                        termSection.Notes = new List<string> { usageNote };
                    }

                    var PartOfSpeech = tigNode
                        .SelectNodes("termNote")?
                        .Cast<XmlElement>()
                        .FirstOrDefault(termNote => termNote.Attributes["type"]?.Value == "partOfSpeech")?
                        .InnerText;

                    if (PartOfSpeech != null && partOfSpeechAbbreviations.TryGetValue(PartOfSpeech, out var fullPartOfSpeech)) {
                        termSection.PartOfSpeech = Enum.Parse<PartOfSpeech>(fullPartOfSpeech);
                    }

                    termSections.Add(termSection);
                }
                
                languageSections.Add(new(language, termSections));
            }

            if (languageSections.Any())
            {
                var id = termEntryNode.Attributes["id"]!.Value;
                var subject = termEntryNode
                    .SelectNodes("descrip")?
                    .Cast<XmlElement>()
                    .ToArray()
                    .FirstOrDefault(descriptionNode => descriptionNode.Attributes["type"]?.Value == "subjectField")
                    ?.InnerText;                
                var definition = languageGroupNodes.Cast<XmlElement>()
                    .FirstOrDefault(node =>
                    {
                        var descriptionWithDefinition = node
                            .SelectNodes("descripGrp/descrip")?
                            .Cast<XmlElement>()
                            .FirstOrDefault(descriptionNode => descriptionNode.Attributes["type"]?.Value == "definition");

                        if (descriptionWithDefinition == null)
                            return false;

                        return true;
                    })?
                    .SelectSingleNode("descripGrp/descrip")!.InnerText;
                
                var entry = new GlossaryConceptEntry(id, languageSections)
                {
                    SubjectField = subject,
                    Definition = definition
                };
            
                conceptEntries.Add(entry);
            }
        }

        return glossary;
    }
}