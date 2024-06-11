namespace Dima.Api;

public static class ApiConfiguration
{
    public const int TenantId = 1;

    // Ajustar pois agora tem login
    public const string UserId = "marcio@jsmengenharia.com.br";

    public const string CorsPolicyName = "wasm";
    public static string StripeApiKey { get; set; } = string.Empty;
}