using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.TrafficControllers
{
    public class DeleteTrafficControllerEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandleAsync)
                .WithName("TrafficControllers: Delete")
                .WithSummary("Exclui um Controlador de Tráfego")
                .WithDescription("Exclui um Controlador de Tráfego")
                .WithOrder(3)
                .Produces<Response<TrafficController?>>();

        private static async Task<IResult> HandleAsync(
            ITrafficControllerHandler handler,
            long id)
        {
            var request = new DeleteTrafficControllerRequest
            {
                TenantId = ApiConfiguration.TenantId,
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
