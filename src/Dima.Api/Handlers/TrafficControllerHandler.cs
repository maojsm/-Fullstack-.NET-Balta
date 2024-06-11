using Microsoft.EntityFrameworkCore;
using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Responses;
using System.ComponentModel.DataAnnotations;

namespace Dima.Api.Handlers
{
    public class TrafficControllerHandler(AppDbContext context) : ITrafficControllerHandler
    {
        public async Task<Response<TrafficController?>> CreateAsync(CreateTrafficControllerRequest request)
        {
            var trafficController = new TrafficController
            {
                TenantId = request.TenantId,
                Name = request.Name,
                CodeLocal = request.CodeLocal,
                Description = request.Description
            };

            try
            {
                await context.TrafficControllers.AddAsync(trafficController);
                await context.SaveChangesAsync();

                return new Response<TrafficController?>(trafficController, 201, "Controlador de tráfego criado com sucesso!");
            }
            catch
            {
                return new Response<TrafficController?>(null, 500, "Não foi possível criar o controlador de tráfego");
            }
        }

        public async Task<Response<TrafficController?>> UpdateAsync(UpdateTrafficControllerRequest request)
        {
            try
            {
                var trafficController = await context
                    .TrafficControllers
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.TenantId == request.TenantId);

                if (trafficController is null)
                    return new Response<TrafficController?>(null, 404, "Controlador de tráfego não encontrado");

                trafficController.CodeLocal = request.CodeLocal;
                trafficController.Name = request.Name;
                trafficController.CodeLocal = request.CodeLocal;
                trafficController.Description = request.Description;

                context.TrafficControllers.Update(trafficController);
                await context.SaveChangesAsync();

                return new Response<TrafficController?>(trafficController, message: "Controlador de tráfego atualizado com sucesso");
            }
            catch (Exception)
            {
                return new Response<TrafficController?>(null, 500, "Não foi possível alterar o controlador de tráfego");
            }
        }

        public async Task<Response<TrafficController?>> DeleteAsync(DeleteTrafficControllerRequest request)
        {
            try
            {
                var trafficController = await context
                 .TrafficControllers
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.TenantId == request.TenantId);

                if (trafficController is null)
                    return new Response<TrafficController?>(null, 404, "Controlador de tráfego não encontrado");

                context.TrafficControllers.Remove(trafficController);
                await context.SaveChangesAsync();

                return new Response<TrafficController?>(trafficController, message: "Controlador de tráfego excluído com sucesso");
            }
            catch (Exception)
            {
                return new Response<TrafficController?>(null, 500, "Não foi possível excluir o controlador de tráfego");
            }
        }


        public async Task<Response<TrafficController?>> GetByIdAsync(GetTrafficControllerByIdRequest request)
        {
            try
            {
                var trafficController = await context
                    .TrafficControllers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.TenantId == request.TenantId);

                return trafficController is null
                    ? new Response<TrafficController?>(null, 404, "Controlador de tráfego não encontrado")
                    : new Response<TrafficController?>(trafficController);
            }
            catch (Exception)
            {
                return new Response<TrafficController?>(null, 500, "Não foi possível buscar o controlador de tráfego");
            }
        }

        public async Task<PagedResponse<List<TrafficController>?>> GetAllAsync(GetAllTrafficControllerRequest request)
        {
            try
            {
                var query = context
                    .TrafficControllers
                    .AsNoTracking()
                    .Where(x => x.TenantId == request.TenantId)
                    .OrderBy(x => x.Id);

                var trafficControllers = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<TrafficController>?>(
                    trafficControllers,
                    count,
                    request.PageNumber,
                    request.PageSize);

            }
            catch (Exception)
            {
                return new PagedResponse<List<TrafficController>?>(null, 500, "Não foi possível buscar os controladores de tráfego");
            }
        }
    }
}
