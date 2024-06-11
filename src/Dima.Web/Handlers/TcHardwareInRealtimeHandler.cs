using Dima.Core.Dtos;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TcHardwareInRealtime;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{

    public class TcHardwareInRealtimeHandler(IHttpClientFactory httpClientFactory) : ITcHardwareInRealtimeHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<TcHardwareInRealtime?>> CreateAsync(CreateTcHardwareInRealtimeRequest request)
        {
            var result = await _client.PostAsJsonAsync("/v1/traffic-controllers/hard-realtimes", request);
            return await result.Content.ReadFromJsonAsync<Response<TcHardwareInRealtime?>>()
                ?? new Response<TcHardwareInRealtime?>(null, 400, "Falha ao criar novo registro de dados do hardware");
        }

        public async Task<Response<List<TcHardwareInRealtimeDto>?>> GetAllAsync(GetAllTcHardwareInRealtimeRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<TcHardwareInRealtimeDto>?>>("/v1/traffic-controllers/hard-realtimes")
                ?? new Response<List<TcHardwareInRealtimeDto>?>(null, 400, "Não foi possível obter os registros de hardware dos controladores");
        }

        public async Task<Response<List<TcHardwareInRealtime>?>> GetAllAsync3(GetAllTcHardwareInRealtimeRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<TcHardwareInRealtime>?>>("/v1/traffic-controllers/hard-realtimes")
                ?? new Response<List<TcHardwareInRealtime>?>(null, 400, "Não foi possível obter os registros de hardware dos controladores");
        }



    }
}
