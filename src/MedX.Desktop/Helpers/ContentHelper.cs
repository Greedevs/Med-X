using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using MedX.WebApi.Models;
using MedX.Service.DTOs.Employees;

namespace MedX.Desktop.Helpers;

public class ContentHelper
{
    public static StringContent GetContent(dynamic content)
        => new(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

    public static async ValueTask<TContent> GetContentAsync<TContent>(HttpResponseMessage responseMessage) where TContent : new()
    {
        var resp = JsonConvert.DeserializeObject<Response>(await responseMessage.Content.ReadAsStringAsync());

        if (resp!.StatusCode == 200)
            return JsonConvert.DeserializeObject<TContent>(resp.Data.ToString());

        MessageBox.Show(resp.Message);
        return new();
    }
}
