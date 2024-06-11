using Microsoft.AspNetCore.Mvc;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Requests.TcHardwareInRealtime;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.TcHardwareInRealtimes
{
    public class GetAllTcHardwareInRealtimeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("TcHardwareInRealtimes: Get All")
                .WithSummary("Recupera todas as informações de hardware em tempo real")
                .WithDescription("Recupera todas as informações de hardware em tempo real")
                .WithOrder(2)
                .Produces<PagedResponse<List<TcHardwareInRealtime>?>>();

        private static async Task<IResult> HandleAsync(
            ITcHardwareInRealtimeHandler handler)
        //[FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        //[FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllTcHardwareInRealtimeRequest
            {
                //UserId = ApiConfiguration.UserId,
                //TenantId = ApiConfiguration.TenantId,
                //PageNumber = pageNumber,
                //PageSize = pageSize,
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
