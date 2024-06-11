using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Requests.TcHardwareInRealtime;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.TcHardwareInRealtimes
{
    public class CreateTcHardwareInRealtimeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("TcHardwareInRealtime: Create")
                .WithSummary("Cria um novo registro do Hardware do Controlador de Tráfego")
                .WithDescription("Cria um novo registro do Hardware do Controlador de Tráfego")
                .WithOrder(1)
                .Produces<Response<TcHardwareInRealtime?>>();

        private static async Task<IResult> HandleAsync(
            ITcHardwareInRealtimeHandler handler,
            CreateTcHardwareInRealtimeRequest request)
        {
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"v1/traffic-controllers/hard-realtimes/{response.Data?.Id}", response)
                : TypedResults.BadRequest(response);
        }
    }
}
