using Microsoft.AspNetCore.Http;
using System.Net;

namespace Dima.Api.Handlers
{
    public static class VisitorPublicIP
    {
        /// <summary>
        /// Forma de uso em Blazor Pages (funiona em API ou programaticamente tbm): 
        /// 
        /// @inject IHttpContextAccessor httpContextAccessor;
        /// 
        /// protected override void OnInitialized()
        /// {
        ///     ipPublicoDoVisitante = VisitorPublicIP.GetIpV4(httpContextAccessor.HttpContext);
        /// }
        /// 
        /// Adicionar em Program.cs:
        /// builder.Services.AddHttpContextAccessor();
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetIpV4(HttpContext? httpContext)
        {
            if (httpContext == null)
            {
                return "0.0.0.0";
            }

            // Verifica se o cabeçalho X-Forwarded-For está presente
            string? forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrEmpty(forwardedFor))
            {
                // Pega o primeiro IP da lista, que é o IP original do cliente
                string? firstIp = forwardedFor.Split(',').FirstOrDefault();
                if (IPAddress.TryParse(firstIp, out IPAddress? ipAddress))
                {
                    return ipAddress.MapToIPv4().ToString();
                }
            }

            // Caso o cabeçalho não esteja presente, usa o IP da conexão remota
            IPAddress? publicIp = httpContext.Connection.RemoteIpAddress?.MapToIPv4();

            return publicIp!.ToString() ?? "0.0.0.0";

        }

        public static int GetPort(HttpContext? httpContext)
        {
            if (httpContext == null)
            {
                return 0;
            }
            return httpContext?.Connection.RemotePort ?? 0;
        }

    }
}
