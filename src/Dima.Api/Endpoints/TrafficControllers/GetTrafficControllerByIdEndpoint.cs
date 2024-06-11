using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.TrafficControllers
{
    public class GetTrafficControllerByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("TrafficControllers: Get By Id")
                .WithSummary("Recupera um Controlador de Tráfego")
                .WithDescription("Recupera um Controlador de Tráfego")
                .WithOrder(4)
                .Produces<Response<TrafficController?>>();

        private static async Task<IResult> HandleAsync(
            ITrafficControllerHandler handler,
            long id)
        {
            var request = new GetTrafficControllerByIdRequest
            {
                TenantId = ApiConfiguration.TenantId,
                //UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
