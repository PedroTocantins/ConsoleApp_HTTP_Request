using ConsoleApp_HTTP_Request.Models;
using System.Collections;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

const string apiUrl = "https://api.dictionaryapi.dev/api/v2/entries/en/";
const string synonymApiUrl = $"https://api.datamuse.com/words?rel_syn=";

Console.Write("Type some word: ");
string word = Console.ReadLine() ?? "default";
string translatedWord = await TranslateWord(word);



using (HttpClient client = new HttpClient())
{
    try
    {
        HttpResponseMessage dictionaryResponse = await client.GetAsync(apiUrl +""+ translatedWord);
        HttpResponseMessage synonymReponse = await client.GetAsync(synonymApiUrl+""+ translatedWord);
        dictionaryResponse.EnsureSuccessStatusCode();

        string dictionaryResponseBody = await dictionaryResponse.Content.ReadAsStringAsync();
        string synonymReponseBody = await synonymReponse.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<DictionaryApiResponse>? entries = JsonSerializer.Deserialize<List<DictionaryApiResponse>>(dictionaryResponseBody, options) ?? null;
        var synonyms = JsonSerializer.Deserialize<List<SynonymResponse>>(synonymReponseBody, options);

        if (entries != null)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Word: {entry.Word}");

                Console.WriteLine("Meanings:");
                foreach (var meaning in entry.Meanings)
                {
                    Console.WriteLine($" - Part of speech: {meaning.PartOfSpeech}");
                    foreach (var definition in meaning.Definitions)
                    {
                        Console.WriteLine($"   Definition: {definition.Definition}");
                        Console.WriteLine($"   Example: {definition.Example}");
                        Console.WriteLine("---------------------------------------------------------------------------------------");

                    }
                }
            }

            Console.WriteLine("Synonyms: ");
            foreach (var synonym in synonyms)
            {
                Console.WriteLine($" - {synonym.Word}");
            }
        }
            
        else
            Console.WriteLine("Something went wrong!");
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine(ex);
    }

}

static async Task<string> TranslateWord(string word)
{
    string url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(word)}&langpair=pt-BR|en";

    using HttpClient client = new();

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };


    HttpResponseMessage response = await client.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
        string jsonResponse = await response.Content.ReadAsStringAsync();

        var translationResult = JsonSerializer.Deserialize<TranslationResponse>(jsonResponse, options);

        return translationResult?.ResponseData?.TranslatedText ?? "Translation Error";
    }
    else
    {
        return "Error in Translation API call";
    }
}

