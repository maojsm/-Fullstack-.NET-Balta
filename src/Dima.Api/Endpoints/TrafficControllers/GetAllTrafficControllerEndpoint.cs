using Microsoft.AspNetCore.Mvc;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Responses;
using Dima.Core;
using Dima.Core.Requests.TrafficController;

namespace Dima.Api.Endpoints.TrafficControllers
{
    public class GetAllTrafficControllerEndpoint : IEndpoint
    {

        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("TrafficControllers: Get All")
                .WithSummary("Recupera todos os Controladores")
                .WithDescription("Recupera todos os Controladores")
                .WithOrder(5)
                .Produces<PagedResponse<List<TrafficController>?>>();

        private static async Task<IResult> HandleAsync(
            ITrafficControllerHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllTrafficControllerRequest
            {
                //UserId = ApiConfiguration.UserId,
                TenantId = ApiConfiguration.TenantId,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
