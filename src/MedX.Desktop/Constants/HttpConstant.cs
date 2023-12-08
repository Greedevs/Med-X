namespace MedX.Desktop.Constants;

public class HttpConstant
{
    private const string domain = "localhost";
    private const string scheme = "http";
    private const string port = "5298";

    public const string BaseLink = $"{scheme}://{domain}:{port}/";
}
