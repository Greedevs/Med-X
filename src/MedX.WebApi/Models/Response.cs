namespace MedX.WebApi.Models;

public class Response
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "Success";
    public object Data { get; set; }
}
