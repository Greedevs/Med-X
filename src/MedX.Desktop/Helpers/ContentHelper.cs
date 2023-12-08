using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using MedX.WebApi.Models;
using System.Windows;

namespace MedX.Desktop.Helpers;

public class ContentHelper
{
    public static StringContent GetContent(dynamic content)
        => new(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

    public static async ValueTask<TContent> GetContentAsync<TContent>(HttpResponseMessage responseMessage) where TContent : class, new()
    {
        var content = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(content);
        if (response!.StatusCode == 200)
            return JsonConvert.DeserializeObject<TContent>(response.Data.ToString()!)!;

        MessageBox.Show(response.Message);
        return new();
    }
}
