using Microsoft.AspNetCore.Builder;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.TrafficControllers
{
    public class UpdateTrafficControllerEndpoint : IEndpoint
    {

        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
                .WithName("TrafficControllers: Update")
                .WithSummary("Atualiza um Controlador de Tráfego")
                .WithDescription("Atualiza um Controlador de Tráfego")
                .WithOrder(2)
                .Produces<Response<TrafficController?>>();

        private static async Task<IResult> HandleAsync(
            ITrafficControllerHandler handler,
            UpdateTrafficControllerRequest request,
            long id)
        {
            request.TenantId = ApiConfiguration.TenantId;
            request.Id = id;
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
