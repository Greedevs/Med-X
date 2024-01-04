using Microsoft.AspNetCore.Http;

namespace MedX.Service.Helpers;

public static class HttpContextHelper
{
    public static IHttpContextAccessor Accessor { get; set; }

    private static HttpContext HttpContext => Accessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long? GetUserId => long.TryParse(HttpContext?.User?.FindFirst("id")?.Value, out _tempUserId) ? _tempUserId : null;
    public static string UserRole => HttpContext?.User?.FindFirst("role")?.Value;

    private static long _tempUserId;
}
