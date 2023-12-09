using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;

namespace MedX.Desktop.Helpers;

public class ContentHelper
{
    public static StringContent GetContent(dynamic content)
        => new(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

    public static async ValueTask<TContent> GetContentAsync<TContent>(HttpResponseMessage response) where TContent : class, new()
        => JsonConvert.DeserializeObject<TContent>((await response.Content.ReadAsStringAsync()) 
            ?? JsonConvert.SerializeObject(new TContent()))!;
}
