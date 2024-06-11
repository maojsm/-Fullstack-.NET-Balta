using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.TrafficControllers;

public class CreateTrafficControllerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("TrafficControllers: Create")
            .WithSummary("Cria um novo Controlador de Tráfego")
            .WithDescription("Cria um novo Controlador de Tráfego")
            .WithOrder(1)
            .Produces<Response<TrafficController?>>();

    private static async Task<IResult> HandleAsync(
        ITrafficControllerHandler handler,
        CreateTrafficControllerRequest request)
    {
        request.TenantId = ApiConfiguration.TenantId;
        var response = await handler.CreateAsync(request);
        return response.IsSuccess
            ? TypedResults.Created($"v1/traffic-controller/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}
