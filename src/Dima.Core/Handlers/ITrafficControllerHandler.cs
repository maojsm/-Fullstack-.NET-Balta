using Dima.Core.Models;
using Dima.Core.Requests.TrafficController;
using Dima.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    public interface ITrafficControllerHandler
    {
        Task<Response<TrafficController?>> CreateAsync(CreateTrafficControllerRequest request);
        Task<Response<TrafficController?>> UpdateAsync(UpdateTrafficControllerRequest request);
        Task<Response<TrafficController?>> DeleteAsync(DeleteTrafficControllerRequest request);
        Task<Response<TrafficController?>> GetByIdAsync(GetTrafficControllerByIdRequest request);
        Task<PagedResponse<List<TrafficController>?>> GetAllAsync(GetAllTrafficControllerRequest request);
    }
}
