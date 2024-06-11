namespace Dima.Core;

public static class Configuration
{
    public const int DefaultStatusCode = 200;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 250;

    // veio do Dima
    //public static string BackendUrl { get; set; } = "https://localhost:7250";
    //public static string FrontendUrl { get; set; } = "https://localhost:7200";


    public static string ConnectionString { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;

    public static long PremiumPrice { get; set; } = 79990;

    public const long TimeMaxForInvalidDataInSeconds = 60;
}