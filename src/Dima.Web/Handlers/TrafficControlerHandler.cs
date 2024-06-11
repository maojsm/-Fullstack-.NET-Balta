using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{

    public class TrafficControlerHandler(IHttpClientFactory httpClientFactory) : ITrafficControllerHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<TrafficController?>> CreateAsync(CreateTrafficControllerRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/traffic-controllers", request);
            return await result.Content.ReadFromJsonAsync<Response<TrafficController?>>()
                ?? new Response<TrafficController?>(null, 400, "Falha ao criar Controlador de Tráfego");
        }

        public async Task<Response<TrafficController?>> DeleteAsync(DeleteTrafficControllerRequest request)
        {
            var result = await _client.DeleteAsync($"v1/traffic-controllers/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<TrafficController?>>()
                   ?? new Response<TrafficController?>(null, 400, "Falha ao criar Controlador de Tráfego");
        }

        public async Task<PagedResponse<List<TrafficController>?>> GetAllAsync(GetAllTrafficControllerRequest request)
        {
            return await _client.GetFromJsonAsync<PagedResponse<List<TrafficController>?>>("v1/traffic-controllers")
                ?? new PagedResponse<List<TrafficController>?>(null, 400, "Não foi possível obter os Controladores de Tráfego");
        }

        public async Task<Response<TrafficController?>> GetByIdAsync(GetTrafficControllerByIdRequest request)
        {
            return await _client.GetFromJsonAsync<Response<TrafficController?>>($"v1/traffic-controllers/{request.Id}") 
                ?? new Response<TrafficController?>(null, 400, "Não foi possível obter o Controlador de Tráfego");
        }

        public async Task<Response<TrafficController?>> UpdateAsync(UpdateTrafficControllerRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/traffic-controllers/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<TrafficController?>>()
                ?? new Response<TrafficController?>(null, 400, "Falha ao atualizar o Controlador de Tráfego");
        }        
    }
}
